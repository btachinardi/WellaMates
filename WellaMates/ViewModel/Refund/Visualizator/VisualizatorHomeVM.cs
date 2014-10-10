
namespace WellaMates.ViewModel
{
    public class VisualizatorHomeVM : BaseMemberVM
    {
        public GetReportVM GetReport;

        public VisualizatorHomeVM()
        {
            GetReport = new GetReportVM();
        }
    }
}