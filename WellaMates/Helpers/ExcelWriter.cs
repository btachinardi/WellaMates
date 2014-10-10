using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using WellaMates.DAL;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.Helpers
{
    public class ExcelWriter
    {
        private readonly ExcelPackage _xlPackage;
        private ExcelWorksheet _currentWorksheet;
        private int _columnPosition;
        private int _rowPosition;

        public ExcelWriter(string name)
        {
            _xlPackage = new ExcelPackage(new FileInfo(name));
        }

        public ExcelWriter OpenWorksheet(string worksheetName)
        {
            if (_xlPackage.Workbook.Worksheets.All(w => w.Name != worksheetName))
            {
                _xlPackage.Workbook.Worksheets.Add(worksheetName);
            }
            _currentWorksheet = _xlPackage.Workbook.Worksheets[worksheetName];
            _rowPosition = 1;
            _columnPosition = 1;
            return this;
        }

        public ExcelWriter Write(object value)
        {
            if (_currentWorksheet == null)
            {
                OpenWorksheet("Planilha");
            }
            var cell = _currentWorksheet.Cells[_columnPosition, _rowPosition];
            cell.Value = value;
            return this;
        }

        public static byte[] GenerateReport(DirectoryInfo templateDir, GetReportVM filters, PortalContext db)
        {
            var title = filters.Title;
            var startDate = filters.StartDate;
            var endDate = filters.EndDate;
            var manager = filters.Manager;
            var templatePath = templateDir.FullName + @"\BasicReportTemplate.xlsx";
            var template = new FileInfo(templatePath);
            if (!template.Exists) throw new Exception("Template file does not exist! Should exist at: " + templatePath);

            using (var xlPackage = new ExcelPackage(new FileInfo(title), template))
            {
                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets["Relatório"];
                if (worksheet != null)
                {
                    const int startRow = 6;
                    var row = startRow;
                    var freelancers = db.Freelancers.Where(f => f.RefundProfile.User.Active).ToList();
                    if (manager != null)
                    {
                        freelancers = freelancers.Where(f => manager.Freelancers.Any(mf => mf.UserID == f.UserID)).ToList();
                    }
                    if (freelancers.Count > 2)
                    {
                        worksheet.InsertRow(row + 1, freelancers.Count - 2);
                    }
                    worksheet.Cells[2, 1].Value = title;
                    foreach (var freelancer in freelancers)
                    {

                        var visits = freelancer.Visits.Where(v => v.Date >= startDate && v.Date <= endDate).ToList();
                        var events = freelancer.Events.Where(e => e.StartDate >= startDate && e.StartDate <= endDate).ToList();
                        var monthlies = freelancer.Monthlies.Where(m => (m.MonthStart >= startDate && m.MonthStart <= endDate) || (m.MonthEnd >= startDate && m.MonthEnd <= endDate)).ToList();
                        var visitsRefundItems = visits.SelectMany(v => v.Refund.RefundItems.Where(ri => ri.Status == RefundItemStatus.PAID || ri.Status == RefundItemStatus.ACCEPTED)).ToList();
                        var eventsRefundItems = events.SelectMany(v => v.Refund.RefundItems.Where(ri => ri.Status == RefundItemStatus.PAID || ri.Status == RefundItemStatus.ACCEPTED)).ToList();
                        var monthliesRefundItems = monthlies.SelectMany(v => v.Refund.RefundItems.Where(ri => ri.Status == RefundItemStatus.PAID || ri.Status == RefundItemStatus.ACCEPTED)).ToList();
                        var refundItems = visitsRefundItems.Concat(eventsRefundItems).ToList();
                        var allItems = refundItems.Concat(monthliesRefundItems).ToList();

                        worksheet.Cells[row, 1].Value = freelancer.RefundProfile.User.PersonalInfo.Name;
                        worksheet.Cells[row, 2].Value = visits.Count;
                        worksheet.Cells[row, 3].Value = events.Count;
                        worksheet.Cells[row, 4].Value = monthlies.Count;
                        worksheet.Cells[row, 5].Value = allItems.Sum(ri => ri.Value);
                        worksheet.Cells[row, 6].Value = monthliesRefundItems.Sum(ri => ri.Value);
                        worksheet.Cells[row, 7].Value = refundItems.Sum(ri => ri.Value);
                        worksheet.Cells[row, 8].Value = refundItems.Where(ri => ri.SubCategory == RefundItemSubCategory.TRANSPORTATION_KM).Sum(ri => ri.Value);
                        worksheet.Cells[row, 9].Value = refundItems.Where(ri => ri.SubCategory == RefundItemSubCategory.TRANSPORTATION_BUS_TICKET).Sum(ri => ri.Value);
                        worksheet.Cells[row, 10].Value = refundItems.Where(ri => ri.SubCategory == RefundItemSubCategory.TRANSPORTATION_TAXI).Sum(ri => ri.Value);
                        worksheet.Cells[row, 11].Value = refundItems.Where(ri => ri.SubCategory == RefundItemSubCategory.TRANSPORTATION_TOOL).Sum(ri => ri.Value);
                        worksheet.Cells[row, 12].Value = refundItems.Where(ri => ri.Category == RefundItemCategory.TRANSPORTATION).Sum(ri => ri.Value);
                        worksheet.Cells[row, 13].Value = refundItems.Where(ri => ri.SubCategory == RefundItemSubCategory.MEAL_LUNCH).Sum(ri => ri.Value);
                        worksheet.Cells[row, 14].Value = refundItems.Where(ri => ri.SubCategory == RefundItemSubCategory.MEAL_DINNER).Sum(ri => ri.Value);
                        worksheet.Cells[row, 15].Value = refundItems.Where(ri => ri.Category == RefundItemCategory.MEAL).Sum(ri => ri.Value);
                        worksheet.Cells[row, 16].Value = refundItems.Where(ri => ri.Category == RefundItemCategory.XEROX_COPY).Sum(ri => ri.Value);
                        worksheet.Cells[row, 17].Value = refundItems.Where(ri => ri.Category == RefundItemCategory.MAIL_SEDEX).Sum(ri => ri.Value);
                        worksheet.Cells[row, 18].Value = refundItems.Where(ri => ri.Category == RefundItemCategory.LUGGAGE).Sum(ri => ri.Value);
                        worksheet.Cells[row, 19].Value = refundItems.Where(ri => ri.Category == RefundItemCategory.OTHER).Sum(ri => ri.Value);
                        row++;
                    }

                    //Sets the styles for the new rows
                    for (var iCol = 1; iCol <= 19; iCol++)
                    {
                        var originalCell = worksheet.Cells[startRow, iCol];
                        for (var iRow = startRow; iRow < row; iRow++)
                        {
                            var cell = worksheet.Cells[iRow, iCol];
                            cell.StyleID = originalCell.StyleID;
                        }
                    }

                    worksheet.HeaderFooter.OddHeader.CenteredText = "Relatório Mensal Wella Educação";
                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                        string.Format("Página {0} de {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                    worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                }

                // set some core property values
                xlPackage.Workbook.Properties.Title = title;
                xlPackage.Workbook.Properties.Author = "Sistema de Reembolso Wella Mates";
                return xlPackage.GetAsByteArray();
            }
        }
    }
}