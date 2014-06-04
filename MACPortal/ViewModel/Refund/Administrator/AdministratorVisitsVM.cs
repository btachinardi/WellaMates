
using PagedList;
using WellaMates.Models;

namespace WellaMates.ViewModel
{
    public class AdministratorVisitsVM : BaseMemberVM
    {
        public IPagedList<Visit> Visits;
    }
}