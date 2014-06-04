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

        public ResponseVM() : base("Enviar", "SendResponse", "Freelancer", "Events", "Freelancer")
        {
            
        }

        public Refund Refund
        {
            get { return _Refund; }
            set
            {
                _Refund = new Refund
                {
                    Value = value.Value,
                    AcceptedValue = value.AcceptedValue,
                    RefundID = value.RefundID,
                    RefundItems = value.RefundItems.Select(ri => new RefundItem
                    {
                        Activity = ri.Activity,
                        Category = ri.Category,
                        Files = ri.Files,
                        ReceivedInvoice = ri.ReceivedInvoice,
                        RefundID = ri.RefundID,
                        RefundItemID = ri.RefundItemID,
                        Status = ri.Status,
                        Value = ri.Value,
                        OtherSpecification = ri.OtherSpecification
                    }).ToList()
                };
            }
        }

        private Refund _Refund;
        public RefundItemUpdate[] Updates { get; set; }
        public int OwnerID { get; set; }
        public ResponseOwnerType OwnerType { get; set; }
        public bool AllowAttachments { get; set; }
    }

    public enum ResponseOwnerType
    {
        VISIT = 1,
        MONTHLY = 2,
        EVENT = 3
    }
}