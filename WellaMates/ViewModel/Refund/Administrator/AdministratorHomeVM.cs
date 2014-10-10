
namespace WellaMates.ViewModel
{
    public class AdministratorHomeVM : BaseMemberVM
    {
        public GetReportVM GetReport;

        public AdministratorHomeVM()
        {
            GetReport = new GetReportVM();
        }
    }
}