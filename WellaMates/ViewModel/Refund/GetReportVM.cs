using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WellaMates.Models;

namespace WellaMates.ViewModel
{
    public class GetReportVM
    {
        private DateTime _startDate;
        private DateTime _endDate;

        [DisplayName("A partir de")]
        public DateTime StartDate { 
            get { return _startDate; } 
            set { _startDate = value == DateTime.MinValue ? value : new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, 0); } 
        }

        [DisplayName("Até")]
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value == DateTime.MaxValue ? value : new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 999); } 
        }

        public Manager Manager;

        public GetReportVM()
        {
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MaxValue;
        }

        public string Title
        {
            get
            {
                if (StartDate == DateTime.MinValue && EndDate == DateTime.MaxValue)
                {
                    return "Relatorio Wella Educacao Geral";
                }
                return string.Format("Relatorio Wella Educacao{0}{1}",
                    StartDate == DateTime.MinValue ? "" : (" a partir de " + StartDate.ToString("dd-MM-yy")),
                    EndDate == DateTime.MaxValue ? "" : (" ate " + EndDate.ToString("dd-MM-yy")));
            }
        }
    }
}