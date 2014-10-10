using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using JetBrains.Annotations;
using WellaMates.Models;

namespace WellaMates.ViewModel
{
    public class CreateVisitVM : CreateGenericVM
    {
        public readonly Visit Visit;

        public CreateVisitVM(string submitText, [AspMvcAction]string submitAction, [AspMvcController]string submitController = "Freelancer", [AspMvcAction]string successAction = "Visits", [AspMvcController]string successController = "Freelancer")
            : base(submitText, submitAction, submitController, successAction, successController)
        {
            Visit = new Visit
            {
                Date = DateTime.Now,
                Refund = new Refund {
                    RefundItems = new Collection<RefundItem>()
                }
            };
        }
    }
}