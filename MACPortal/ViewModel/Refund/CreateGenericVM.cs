using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using JetBrains.Annotations;
using WellaMates.Models;

namespace MACPortal.ViewModel
{
    public class CreateGenericVM
    {

        public readonly string SubmitText;
        public readonly string SubmitAction;
        public readonly string SubmitController;
        public readonly string SuccessAction;
        public readonly string SuccessController;

        public CreateGenericVM(string submitText, [AspMvcAction]string submitAction, [AspMvcController]string submitController, [AspMvcAction]string successAction, [AspMvcController]string successController)
        {
            SubmitText = submitText;
            SubmitAction = submitAction;
            SubmitController = submitController;
            SuccessAction = successAction;
            SuccessController = successController;
        }
    }
}