using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.ViewModel
{
    public class ManagerRefundItemDetailVM : BaseMemberVM
    {
        public RefundItem RefundItem { get; set; }
    }
}