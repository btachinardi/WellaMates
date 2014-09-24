using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using OfficeOpenXml;
using WebMatrix.WebData;
using WellaMates.Models;
using WellaMates.DAL;

namespace WellaMates.Helpers
{
    public class ExcellHelper
    {
        private int rowNumber;
        private ExcelWorksheet currentWorksheet;
        private Dictionary<string, int> sheetHeaders;
        private readonly PortalContext currentDb;
        private readonly SimpleRoleProvider roles;
        private readonly SimpleMembershipProvider membership;
        private readonly ExcelWorkbook currentWorkBook;

        public ExcellHelper(ExcelWorkbook workBook, PortalContext db)
        {
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
                var refundProfile = ProcessRefundProfile(new[] { "RefundAdministrator" });
                if (refundProfile == null) continue;

                var currentRefundAdmin = refundProfile.RefundAdministrator;
                var isActive = IsActive();
                if (isActive && currentRefundAdmin == null)
                {
                    currentRefundAdmin = new RefundAdministrator
                    {
                        UserID = refundProfile.UserID
                    };
                    currentDb.RefundAdministrators.Add(currentRefundAdmin);
                }
                else if (!isActive && currentRefundAdmin != null)
                {
                    currentDb.RefundAdministrators.Remove(currentRefundAdmin);
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
                var refundProfile = ProcessRefundProfile(new[] { "Freelancer" });
                if (refundProfile == null) continue;

                var currentFreelancer = refundProfile.Freelancer;

                if (!IsActive())
                {
                    currentDb.Freelancers.Remove(currentFreelancer);
                }
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
                var refundProfile = ProcessRefundProfile(new[] { "Manager" });
                if (refundProfile == null) continue;

                var currentManager = refundProfile.Manager;
                var isActive = IsActive();
                if (isActive && currentManager == null)
                {
                    currentManager = new Manager
                    {
                        UserID = refundProfile.UserID,
                        Identification = GetValue("IDENTIFICAÇÃO")
                    };
                    currentDb.Managers.Add(currentManager);
                }
                else if (!isActive && currentManager != null)
                {
                    currentDb.Managers.Remove(currentManager);
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
                var refundProfile = ProcessRefundProfile(new[] { "RefundVisualisation" });
                if (refundProfile == null) continue;

                var currentRefundAdmin = refundProfile.RefundAdministrator;
                var isActive = IsActive();
                if (isActive && currentRefundAdmin == null)
                {
                    currentRefundAdmin = new RefundAdministrator
                    {
                        UserID = refundProfile.UserID
                    };
                    currentDb.RefundAdministrators.Add(currentRefundAdmin);
                }
                else if (!isActive && currentRefundAdmin != null)
                {
                    currentDb.RefundAdministrators.Remove(currentRefundAdmin);
                }
            }
            currentDb.SaveChanges();
        }

        private RefundProfile ProcessRefundProfile(string[] userRoles)
        {
            var userProfile = ProcessUserProfile(userRoles);
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

        private UserProfile ProcessUserProfile(string[] userRoles)
        {
            var CPF = GetValue("CPF").Replace(".", String.Empty).Replace("-", String.Empty).Trim();
            var currentUser = currentDb.UserProfiles.FirstOrDefault(e => e.UserName == CPF);
            if (currentUser != null)
            {
                return currentUser;
            }
            
            if (membership.GetUser(CPF, false) == null)
            {
                membership.CreateUserAndAccount(CPF, CPF);
                currentUser = currentDb.UserProfiles.First(e => e.UserName == CPF);
                currentUser.PersonalInfo.CPF = CPF;
                currentUser.PersonalInfo.Name = GetValue("NOME");
                currentUser.ContactInfo.Email = GetValue("Email");
            }

            var userRolesList = userRoles.ToList();
            userRolesList.Add("Member");
            foreach (var userRole in userRolesList.Where(userRole => roles.IsUserInRole(CPF, userRole)))
            {
                userRolesList.Remove(userRole);
            }
            if (userRolesList.Any())
            {
                roles.AddUsersToRoles(new[] { CPF }, userRoles);
            }

            return currentUser;
        }

        private string GetValue(string fieldName)
        {
            var value = currentWorksheet.Cells[rowNumber, sheetHeaders[fieldName]].Value;
            return value == null ? null : value.ToString();
        }

        private bool IsActive()
        {
            var active = GetValue("Ativo").ToLower();
            return !String.IsNullOrEmpty(active) && (active == "sim" || active == "s");
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
    }
}