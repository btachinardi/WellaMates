using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using WellaMates.ViewModel;
using WellaMates.Models;

namespace WellaMates.ViewModel
{
    public class FreelancerMonthlyCreateVM : BaseMemberVM
    {
        public CreateMonthlyVM CreateMonthly;

        public FreelancerMonthlyCreateVM(CreateMonthlyVM createMonthly)
        {
            CreateMonthly = createMonthly;
        }
    }
}