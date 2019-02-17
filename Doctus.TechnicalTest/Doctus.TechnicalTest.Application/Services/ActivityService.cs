namespace Doctus.TechnicalTest.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Doctus.TechnicalTest.Domain.Entities;
    using Doctus.TechnicalTest.Infrastructure.Framework.Instrumentation.Logging;
    using Doctus.TechnicalTest.Infrastructure.Framework.RepositoryPattern;

    public class ActivityService : IActivityService
    {
        IRepository<Activity> activityRepository;
        ILoggerService loggerService;

        public ActivityService(IRepository<Activity> activityRepository, ILoggerService loggerService)
        {
            this.activityRepository = activityRepository;
            this.loggerService = loggerService;
        }

        public Activity AddActivity(Activity activity)
        {
            try
            {
                activity.CreationDate = DateTime.Now;
                activityRepository.Add(activity);
                activityRepository.Commit();
                return activity;
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                throw;
            }
        }

        public IEnumerable<Activity> GetActivities(int userId)
        {
            return activityRepository.Get(a => a.UserId == userId, a => a.OrderByDescending(o => o.CreationDate));
        }
    }
}
