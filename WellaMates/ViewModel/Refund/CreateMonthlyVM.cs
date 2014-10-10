using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using JetBrains.Annotations;
using WellaMates.Models;

namespace WellaMates.ViewModel
{
    public class CreateMonthlyVM : CreateGenericVM
    {
        public readonly Monthly Monthly;

        public CreateMonthlyVM(Freelancer freelancer, string submitText, [AspMvcAction]string submitAction, [AspMvcController]string submitController = "Freelancer", [AspMvcAction]string successAction = "Monthlies", [AspMvcController]string successController = "Freelancer")
            : base(submitText, submitAction, submitController, successAction, successController)
        {
            var now = DateTime.Now;
            Monthly = new Monthly
            {
                Month = (Month)now.Month,
                Year = now.Year,
                Refund = new Refund {
                    RefundItems = new Collection<RefundItem>
                    {
                        new RefundItem
                        {
                            Activity = "Reembolso Mensal",
                            Category = RefundItemCategory.SALARY,
                            Files = new Collection<File>(),
                            Value = freelancer.Remuneration
                        }
                    }
                }
            };
        }
    }
}