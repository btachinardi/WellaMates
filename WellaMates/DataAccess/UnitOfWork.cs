using System;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using WellaMates.DAL;
using WellaMates.Extensions;
using WellaMates.Models;

namespace WellaMates.Code.DataAccess
{
    public enum UnitOfWorkMode
    {
        Freelancer,
        Manager,
        Administrator
    }

    public class UnitOfWork : IDisposable
    {
        #region Repositories

        protected IRepository<Event> EventRepository;
        public IRepository<Event> GetEventRepository
        {
            get
            {
                if (EventRepository == null)
                {
                    Func<string, Expression<Func<Event, bool>>> searchProvider = s => (e =>
                        e.Freelancer.RefundProfile.User.PersonalInfo.Name.Contains(s) ||
                        SqlFunctions.StringConvert((decimal)e.RefundID).Contains(s) ||
                        e.Name.Contains(s)
                    );
                    if (User.RefundProfile == null)
                    {
                        EventRepository = new Repository<Event>(Context, p => false, searchProvider);
                    }
                    else 
                    {
                        if (Mode == UnitOfWorkMode.Administrator)
                        {
                            EventRepository = new Repository<Event>(Context, e => (int)e.Refund.Status != (int)RefundStatus.NON_EXISTENT, searchProvider);
                        }
                        else if (Mode == UnitOfWorkMode.Freelancer)
                        {
                            EventRepository = new Repository<Event>(Context, e => e.FreelancerID == User.UserID, searchProvider);
                        }
                        else if (Mode == UnitOfWorkMode.Manager)
                        {
                            var ids = User.RefundProfile.Manager.Freelancers.Select(f => f.UserID).ToArray();
                            EventRepository = new Repository<Event>(Context, e => ids.Any(id => e.FreelancerID == id),
                                searchProvider);
                        }
                    }
                }
                return EventRepository;
            }
        }

        protected IRepository<Monthly> MonthlyRepository;
        public IRepository<Monthly> GetMonthlyRepository
        {
            get
            {
                if (MonthlyRepository == null)
                {
                    Func<string, Expression<Func<Monthly, bool>>> searchProvider = s => (e =>
                        e.Freelancer.RefundProfile.User.PersonalInfo.Name.Contains(s) ||
                        SqlFunctions.StringConvert((decimal)e.RefundID).Contains(s) ||
                        e.Month.DisplayName().Contains(s)
                    );
                    if (User.RefundProfile == null)
                    {
                        MonthlyRepository = new Repository<Monthly>(Context, p => false, searchProvider);
                    }
                    else
                    {
                        if (Mode == UnitOfWorkMode.Administrator)
                        {
                            MonthlyRepository = new Repository<Monthly>(Context, e => (int)e.Refund.Status != (int)RefundStatus.NON_EXISTENT, searchProvider);
                        }
                        else if (Mode == UnitOfWorkMode.Freelancer)
                        {
                            MonthlyRepository = new Repository<Monthly>(Context, e => e.FreelancerID == User.UserID, searchProvider);
                        }
                        else if (Mode == UnitOfWorkMode.Manager)
                        {
                            var ids = User.RefundProfile.Manager.Freelancers.Select(f => f.UserID).ToArray();
                            MonthlyRepository = new Repository<Monthly>(Context, e => ids.Any(id => e.FreelancerID == id),
                                searchProvider);
                        }
                    }
                }
                return MonthlyRepository;
            }
        }

        protected IRepository<Visit> VisitRepository;
        public IRepository<Visit> GetVisitRepository
        {
            get
            {
                if (VisitRepository == null)
                {
                    Func<string, Expression<Func<Visit, bool>>> searchProvider = s => (e =>
                        e.Freelancer.RefundProfile.User.PersonalInfo.Name.Contains(s) ||
                        SqlFunctions.StringConvert((decimal)e.RefundID).Contains(s)
                    );
                    if (User.RefundProfile == null)
                    {
                        VisitRepository = new Repository<Visit>(Context, p => false, searchProvider);
                    }
                    else
                    {
                        if (Mode == UnitOfWorkMode.Administrator)
                        {
                            VisitRepository = new Repository<Visit>(Context, e => (int)e.Refund.Status != (int)RefundStatus.NON_EXISTENT, searchProvider);
                        }
                        else if (Mode == UnitOfWorkMode.Freelancer)
                        {
                            VisitRepository = new Repository<Visit>(Context, e => e.FreelancerID == User.UserID, searchProvider);
                        }
                        else if (Mode == UnitOfWorkMode.Manager)
                        {
                            var ids = User.RefundProfile.Manager.Freelancers.Select(f => f.UserID).ToArray();
                            VisitRepository = new Repository<Visit>(Context, e => ids.Any(id => e.FreelancerID == id),
                                searchProvider);
                        }
                    }
                }
                return VisitRepository;
            }
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (typeof(T) == typeof(Visit))
            {
                return (IRepository<T>)GetVisitRepository;
            }
            if (typeof(T) == typeof(Event))
            {
                return (IRepository<T>)GetEventRepository;
            }
            if (typeof(T) == typeof(Monthly))
            {
                return (IRepository<T>)GetMonthlyRepository;
            }
            throw new Exception("Target class T '" + typeof(T) + "' is not mapped to any repository.");
        }

        #endregion

        #region UnitOfWork

        protected PortalContext Context;
        private bool _disposed;
        private UnitOfWorkMode Mode;
        private UserProfile User;

        public UnitOfWork(UserProfile user, UnitOfWorkMode mode)
        {
            User = user;
            Mode = mode;
            Context = new PortalContext();
            _disposed = false;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            if(!_disposed)
            {
                Context.Dispose();
                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}