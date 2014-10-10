using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OfficeOpenXml;
using WebMatrix.WebData;
using WellaMates.Models;
using WellaMates.DAL;

namespace WellaMates.Helpers
{
    public class ExcelHelper
    {
        private int rowNumber;
        private ExcelWorksheet currentWorksheet;
        private Dictionary<string, int> sheetHeaders;
        private readonly PortalContext currentDb;
        private readonly SimpleRoleProvider roles;
        private readonly SimpleMembershipProvider membership;
        private readonly ExcelWorkbook currentWorkBook;
        private readonly Dictionary<string, UserRegister> _usersRegister;

        public ExcelHelper(ExcelWorkbook workBook, PortalContext db)
        {
            _usersRegister = new Dictionary<string, UserRegister>();
            currentWorkBook = workBook;
            roles = (SimpleRoleProvider)Roles.Provider;
            membership = (SimpleMembershipProvider)Membership.Provider;
            currentDb = db;
        }

        public void ProcessRefundAdministrator()
        {
            if (!UpdateWorksheet("Administradores")) return;
            for (rowNumber = 2; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
            {
                if (currentWorksheet.Cells[rowNumber, 1].Value == null)
                {
                    continue;
                }
                var refundProfile = ProcessRefundProfile("RefundAdministrator");
                if (refundProfile == null) continue;

                var currentRefundAdmin = refundProfile.RefundAdministrator;
                if (currentRefundAdmin == null)
                {
                    currentRefundAdmin = new RefundAdministrator
                    {
                        UserID = refundProfile.UserID
                    };
                    currentDb.RefundAdministrators.Add(currentRefundAdmin);
                }
            }
            currentDb.SaveChanges();
        }

        public void ProcessFreelancer()
        {
            if (!UpdateWorksheet("Freelancers")) return;
            for (rowNumber = 2; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
            {
                if (currentWorksheet.Cells[rowNumber, 1].Value == null)
                {
                    continue;
                }
                var refundProfile = ProcessRefundProfile("Freelancer");
                if (refundProfile == null) continue;

                var currentFreelancer = refundProfile.Freelancer;

                if (currentFreelancer != null) continue;

                if (currentFreelancer == null)
                {
                    currentFreelancer = new Freelancer
                    {
                        UserID = refundProfile.UserID,
                        WorkDays = Convert.ToInt32(GetValue("DIAS TRABALHADOS P/MÊS")),
                        Remuneration = Convert.ToDecimal(GetValue("REMUNERAÇÃO")),
                        MealAssistance = Convert.ToDecimal(GetValue("AUX. REFEIÇÃO")),
                        TelephoneAssistance = Convert.ToDecimal(GetValue("TELEFONIA")),
                        Type = Freelancer.GetType(GetValue("Cargo"))
                    };
                }

                var managersNames = GetValue("SUPERVISOR").Split('/').ToList();
                var managers = currentDb.Managers.Where(m => managersNames.Any(name => name.ToLower().Equals(m.Identification.ToLower()))).ToList();
                currentFreelancer.Managers = managers;
                currentDb.Freelancers.Add(currentFreelancer);
            }
            currentDb.SaveChanges();
        }

        public void ProcessManager()
        {
            if (!UpdateWorksheet("Supervisores")) return;
            for (rowNumber = 2; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
            {
                if (currentWorksheet.Cells[rowNumber, 1].Value == null)
                {
                    continue;
                }
                var refundProfile = ProcessRefundProfile("Manager");
                if (refundProfile == null) continue;

                var currentManager = refundProfile.Manager;
                if (currentManager == null)
                {
                    currentManager = new Manager
                    {
                        UserID = refundProfile.UserID,
                        Identification = GetValue("IDENTIFICAÇÃO")
                    };
                    currentDb.Managers.Add(currentManager);
                }
            }
            currentDb.SaveChanges();
        }

        public void ProcessVisualization()
        {
            if (!UpdateWorksheet("Visualizadores")) return;
            for (rowNumber = 2; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
            {
                if (currentWorksheet.Cells[rowNumber, 1].Value == null)
                {
                    continue;
                }
                var refundProfile = ProcessRefundProfile("RefundVisualizator");
                if (refundProfile == null) continue;

                var currentRefundVis = refundProfile.RefundVisualizator;
                if (currentRefundVis == null)
                {
                    currentRefundVis = new RefundVisualizator
                    {
                        UserID = refundProfile.UserID
                    };
                    currentDb.RefundVisualizators.Add(currentRefundVis);
                }
            }
            currentDb.SaveChanges();
        }

        public void ProcessUsersRoles()
        {
            foreach (var CPF in _usersRegister.Keys)
            {
                var userRoles = _usersRegister[CPF].Roles;
                var cpf = CPF;
                var newRoles = userRoles.Where(userRole => !roles.IsUserInRole(cpf, userRole));
                var removedRoles = Roles.GetRolesForUser(CPF).Where(userRole => !userRoles.Contains(userRole));

                foreach (var userRole in newRoles)
                {
                    Roles.AddUserToRole(CPF, userRole);
                }

                foreach (var userRole in removedRoles)
                {
                    Roles.RemoveUserFromRole(CPF, userRole);
                }
            }
        }

        private RefundProfile ProcessRefundProfile(string role)
        {
            var userProfile = ProcessUserProfile(role);
            if (userProfile == null) return null;

            var currentRefund = userProfile.RefundProfile;
            if (currentRefund != null) return currentRefund;

            currentRefund = new RefundProfile
            {
                UserID = userProfile.UserID
            };
            currentDb.RefundProfiles.Add(currentRefund);
            return currentRefund;
        }

        private UserProfile ProcessUserProfile(string role)
        {
            var CPF = GetValue("CPF").Replace(".", String.Empty).Replace("-", String.Empty).Trim();
            var userRegister = _usersRegister.ContainsKey(CPF)
                ? _usersRegister[CPF]
                : _usersRegister[CPF] = new UserRegister(GetUserProfile(CPF));
            userRegister.Roles.Add(role);
            return userRegister.Profile;
        }

        private UserProfile GetUserProfile(string CPF)
        {
            var currentUser = currentDb.UserProfiles.FirstOrDefault(e => e.UserName == CPF);
            if (currentUser == null)
            {
                if (membership.GetUser(CPF, false) != null)
                {
                    throw new Exception("User '" + CPF + "' was not found under UserProfiles but is already registered as a membership user.");
                }
                membership.CreateUserAndAccount(CPF, CPF);
                currentUser = currentDb.UserProfiles.First(e => e.UserName == CPF);
            }
            currentUser.PersonalInfo.CPF = CPF;
            currentUser.PersonalInfo.Name = GetValue("NOME");
            currentUser.ContactInfo.Email = GetValue("Email");
            currentUser.Active = GetBoolValue("Ativo");
            currentDb.Entry(currentUser).CurrentValues.SetValues(currentUser);
            return currentUser;
        }



        private bool GetBoolValue(string fieldName)
        {
            var value = currentWorksheet.Cells[rowNumber, sheetHeaders[fieldName]].Value;
            return value != null && value.ToString().ToUpper() == "SIM";
        }

        private string GetValue(string fieldName)
        {
            var value = currentWorksheet.Cells[rowNumber, sheetHeaders[fieldName]].Value;
            return value == null ? null : value.ToString();
        }

        private bool UpdateWorksheet(string sheetName)
        {
            currentWorksheet = currentWorkBook.Worksheets.FirstOrDefault(e => e.Name == sheetName);
            if (currentWorksheet == null) return false;

            sheetHeaders = new Dictionary<string, int>();
            for (var columnNumber = 1;
                columnNumber <= currentWorksheet.Dimension.End.Column;
                columnNumber++)
            {
                var header = currentWorksheet.Cells[1, columnNumber];
                if (header == null || header.Value == null) continue;
                sheetHeaders.Add((string)header.Value, columnNumber);
            }
            return true;
        }


        public void CleanupUsers()
        {
            foreach (var userProfile in currentDb.UserProfiles.ToList())
            {
                if (!_usersRegister.ContainsKey(userProfile.UserName))
                {
                    userProfile.Active = false;
                    currentDb.Entry(userProfile).CurrentValues.SetValues(userProfile);
                }
            }
            currentDb.SaveChanges();
        }
    }

    class UserRegister
    {
        public List<string> Roles { get; private set; }
        public UserProfile Profile { get; private set; }

        public UserRegister(UserProfile profile)
        {
            Roles = new List<string>{"Member"};
            Profile = profile;
        }

    }
}