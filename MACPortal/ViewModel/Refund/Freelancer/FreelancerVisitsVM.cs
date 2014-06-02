
using PagedList;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.ViewModel
{
    public class FreelancerVisitsVM : BaseMemberVM
    {
        public IPagedList<Visit> Visits;
    }
}