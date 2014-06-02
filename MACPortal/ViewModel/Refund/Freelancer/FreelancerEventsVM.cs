
using PagedList;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.ViewModel
{
    public class FreelancerEventsVM : BaseMemberVM
    {
        public IPagedList<Event> Events;
    }
}