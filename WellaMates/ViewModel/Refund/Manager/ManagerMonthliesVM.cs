
using PagedList;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.ViewModel
{
    public class ManagerMonthliesVM : BaseMemberVM
    {
        public IPagedList<Monthly> Monthlies;
    }
}