using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellaMates.ViewModel
{
    public class BaseMemberVM
    {
        public bool IsFreelancer { get; set; }
        public bool IsManager { get; set; }
        public bool IsRefundAdmin { get; set; }
        public bool IsRefundVisualizator { get; set; }
        public bool IsMultiRoles { get; set; }
        public UserVM User { get; set; }
    }
}