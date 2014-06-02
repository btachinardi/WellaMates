
using PagedList;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.ViewModel
{
    public class ManagerEventsVM : BaseMemberVM
    {
        public IPagedList<Event> Events;
    }
}