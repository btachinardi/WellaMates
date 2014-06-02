using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using MACPortal.ViewModel;
using WellaMates.Models;

namespace WellaMates.ViewModel
{
    public class FreelancerVisitCreateVM : BaseMemberVM
    {
        public CreateVisitVM CreateVisit;

        public FreelancerVisitCreateVM(CreateVisitVM createVisit)
        {
            CreateVisit = createVisit;
        }
    }
}