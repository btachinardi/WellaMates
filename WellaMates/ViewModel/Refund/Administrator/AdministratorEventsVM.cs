
using PagedList;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.ViewModel
{
    public class AdministratorEventsVM : BaseMemberVM
    {
        public IPagedList<Event> Events;
    }
}