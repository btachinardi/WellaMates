
using PagedList;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.ViewModel
{
    public class FreelancerMonthliesVM : BaseMemberVM
    {
        public IPagedList<Monthly> Monthlies;
    }
}