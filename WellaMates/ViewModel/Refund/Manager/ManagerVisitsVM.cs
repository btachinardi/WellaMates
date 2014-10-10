
using PagedList;
using WellaMates.Models;

namespace WellaMates.ViewModel
{
    public class ManagerVisitsVM : BaseMemberVM
    {
        public IPagedList<Visit> Visits;
    }
}