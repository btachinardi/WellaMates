using System.Linq;
using JetBrains.Annotations;
using WellaMates.Models;

namespace MACPortal.ViewModel
{
    public class EditRefundVM
    {

        public readonly string SubmitText;
        public readonly string SubmitAction;
        public readonly string SubmitController;
        public readonly string SuccessAction;
        public readonly string SuccessController;
        public readonly Refund Refund;

        public EditRefundVM(Refund Original, string submitText, [AspMvcAction]string submitAction, [AspMvcController]string submitController = "Freelancer", [AspMvcAction]string successAction = "Refunds", [AspMvcController]string successController = "Freelancer")
        {
            SubmitText = submitText;
            SubmitAction = submitAction;
            SubmitController = submitController;
            SuccessAction = successAction;
            SuccessController = successController;

            Refund = new Refund
            {
                RefundID = Original.RefundID,
                RefundItems = Original.RefundItems.Where(r => r.Status != RefundItemStatus.DELETED).Select(r => new RefundItem
                {
                    Activity = r.Activity,
                    Category = r.Category,
                    Status = r.Status,
                    Value = r.Value,
                    RefundID = Original.RefundID,
                    RefundItemID = r.RefundItemID,
                    Files = r.Files
                }).ToList()
            };
        }
    }
}