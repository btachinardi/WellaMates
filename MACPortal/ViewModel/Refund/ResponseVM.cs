using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WellaMates.Models;

namespace MACPortal.ViewModel
{
    public class ResponseVM : CreateGenericVM
    {
        public ResponseVM(string submitText, string submitAction, string submitController, string successAction, string successController) : 
            base(submitText, submitAction, submitController, successAction, successController)
        {
        }

        public Refund Refund { get; set; }
        public RefundItemUpdate[] Updates { get; set; }
        public int OwnerID { get; set; }
        public ResponseOwnerType OwnerType { get; set; }
    }

    public enum ResponseOwnerType
    {
        VISIT = 1,
        MONTHLY = 2,
        EVENT = 3
    }
}