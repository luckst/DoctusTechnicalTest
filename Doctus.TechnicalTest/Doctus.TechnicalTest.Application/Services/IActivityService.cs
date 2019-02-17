
namespace Doctus.TechnicalTest.Application.Services
{
    using Doctus.TechnicalTest.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IActivityService
    {
        IEnumerable<Activity> GetActivities(int userId);
        Activity AddActivity(Activity activity);
    }
}
