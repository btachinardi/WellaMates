using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.ViewModel
{
    public class ManagerEventDetailVM : BaseMemberVM
    {
        public Event Event;
        public ResponseVM Response;
    }
}