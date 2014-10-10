using System.Collections.ObjectModel;
using System.Web.Security;
using WellaMates.Helpers;
using WebMatrix.WebData;
using WellaMates.DAL;
using WellaMates.Models;

namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PortalContext>
    {
        PortalContext db = new PortalContext();
        Random random = new Random();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        string RandomLipsum()
        {
            var lib = new[]
            {
                "Lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipisicing", "elit", "sed", "do", "eiusmod",
                "tempor", "incididunt", "ut", "labore", "et", "dolore", "magna", "aliqua"
            };
            var lipsum = "";
            while (lipsum.Length < 10 || random.Next(4) > 0)
            {
                var nextWord = lib[random.Next(lib.Count())];
                if (nextWord.Length + lipsum.Length >= 40) continue;
                lipsum += " " + nextWord;
            }
            return lipsum;
        }

        DateTime RandomDay()
        {
            var start = new DateTime(1995, 1, 1);

            var range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        private SimpleRoleProvider roles;
        private SimpleMembershipProvider membership;

        protected override void Seed(PortalContext context)
        {
            WebSecurity.InitializeDatabaseConnection("PortalContext",
               "UserProfile", "UserId", "UserName", autoCreateTables: true);
            roles = (SimpleRoleProvider)Roles.Provider;
            membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (!roles.RoleExists("Member"))
            {
                roles.CreateRole("Member");
            }
            if (!roles.RoleExists("RefundVisualizator"))
            {
                roles.CreateRole("RefundVisualizator");
            }
            if (!roles.RoleExists("RefundAdministrator"))
            {
                roles.CreateRole("RefundAdministrator");
            }
            if (!roles.RoleExists("Manager"))
            {
                roles.CreateRole("Manager");
            }
            if (!roles.RoleExists("Freelancer"))
            {
                roles.CreateRole("Freelancer");
            }

            if (membership.GetUser("admin", false) == null)
            {
                membership.CreateUserAndAccount("admin", "pesca160064");
            }
            if (!roles.GetRolesForUser("admin").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "admin" }, new[] { "Admin" });
            }

            var currentCpf = "38287346851";
            CheckUser(currentCpf, "Bruno Tachinardi Andrade Silva", "brunotachinardi@hotmail.com");
            CheckRefund(currentCpf);
            CheckRefundAdministrator(currentCpf);
            CheckManager(currentCpf, "BRUNO");
            CheckFreelancer(currentCpf, "BRUNO", 8, 1200, 128, 50, FreelancerType.EDUCATOR);

            currentCpf = "17858466801";
            CheckUser(currentCpf, "Pedro de Almeida Pereira", "pedrofakeaccount@fake.com");
            CheckRefund(currentCpf);
            CheckFreelancer(currentCpf, "BRUNO", 8, 1200, 128, 50, FreelancerType.EDUCATOR);

        }

        public void CheckUser(string cpf, string name, string email)
        {
            if (membership.GetUser(cpf, false) == null)
            {
                membership.CreateUserAndAccount(cpf, cpf);
                var user = db.UserProfiles.First(up => up.UserName == cpf);
                user.PersonalInfo.CPF = cpf;
                user.PersonalInfo.Name = name;
                user.ContactInfo.Email = email;
                db.SaveChanges();
            }
            if (!roles.GetRolesForUser(cpf).Contains("Member"))
            {
                roles.AddUsersToRoles(new[] { cpf }, new[] { "Member" });
            }
        }

        public void CheckRefund(string cpf)
        {
            var user = db.UserProfiles.First(up => up.UserName == cpf);
            if (user.RefundProfile == null)
            {
                user.RefundProfile = new RefundProfile{UserID = user.UserID};
                db.SaveChanges();
            }
        }

        public void CheckFreelancer(string cpf, string supervisors, int workDays, decimal remuneration, decimal mealAssistance, decimal telephoneAssistance, FreelancerType type)
        {
            var user = db.UserProfiles.First(up => up.UserName == cpf);
            if (user.RefundProfile.Freelancer == null)
            {
                user.RefundProfile.Freelancer = new Freelancer
                {
                    UserID = user.UserID,
                    WorkDays = workDays,
                    Remuneration = remuneration,
                    MealAssistance = mealAssistance,
                    TelephoneAssistance = telephoneAssistance,
                    Type = type
                };
                
                var managersNames = supervisors.Split('/').ToList();
                var managers = db.Managers.ToList().Where(m => managersNames.Any(name => name.ToLower().Equals(m.Identification.ToLower()))).ToList();
                user.RefundProfile.Freelancer.Managers = managers;
                db.SaveChanges();
            }
        }

        public void CheckManager(string cpf, string identificationName)
        {
            var user = db.UserProfiles.First(up => up.UserName == cpf);
            if (user.RefundProfile.Manager == null)
            {
                user.RefundProfile.Manager = new Manager
                {
                    UserID = user.UserID,
                    Identification = identificationName
                };
                db.SaveChanges();
            }
        }

        public void CheckRefundAdministrator(string cpf)
        {
            var user = db.UserProfiles.First(up => up.UserName == cpf);
            if (user.RefundProfile.RefundAdministrator == null)
            {
                user.RefundProfile.RefundAdministrator = new RefundAdministrator
                {
                    UserID = user.UserID
                };
                db.SaveChanges();
            }
        }
    }
}
