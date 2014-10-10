
namespace WellaMates.ViewModel
{
    public class ManagerHomeVM : BaseMemberVM
    {
        public GetReportVM GetReport;

        public ManagerHomeVM()
        {
            GetReport = new GetReportVM();
        }
    }
}