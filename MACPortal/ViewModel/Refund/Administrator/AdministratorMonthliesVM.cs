
using PagedList;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.ViewModel
{
    public class AdministratorMonthliesVM : BaseMemberVM
    {
        public IPagedList<Monthly> Monthlies;
    }
}