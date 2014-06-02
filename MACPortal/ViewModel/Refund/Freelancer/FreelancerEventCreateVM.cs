using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using MACPortal.ViewModel;
using WellaMates.Models;

namespace WellaMates.ViewModel
{
    public class FreelancerEventCreateVM : BaseMemberVM
    {
        public CreateEventVM CreateEvent;

        public FreelancerEventCreateVM(CreateEventVM createEvent)
        {
            CreateEvent = createEvent;
        }
    }
}