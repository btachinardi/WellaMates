using System;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using WellaMates.Models;

namespace WellaMates.ViewModel
{
    public class CreateEventVM : CreateGenericVM
    {
        public readonly Event Event;

        public CreateEventVM(string submitText, [AspMvcAction]string submitAction, [AspMvcController]string submitController = "Freelancer", [AspMvcAction]string successAction = "Events", [AspMvcController]string successController = "Freelancer")
            : base(submitText, submitAction, submitController, successAction, successController)
        {
            Event = new Event
            {
                Name = "",
                Comments = "",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Refund = new Refund {
                    RefundItems = new Collection<RefundItem>()
                }
            };
        }
    }
}