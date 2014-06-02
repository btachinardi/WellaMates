using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MACPortal.ViewModel;
using PagedList;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.ViewModel
{
    public class FreelancerVisitDetailVM : BaseMemberVM
    {
        public Visit Visit;
        public ResponseVM Response;
    }
}