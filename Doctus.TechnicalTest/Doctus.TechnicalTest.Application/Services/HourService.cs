namespace Doctus.TechnicalTest.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Doctus.TechnicalTest.Domain.Entities;
    using Doctus.TechnicalTest.Infrastructure.Framework.Instrumentation.Exceptions;
    using Doctus.TechnicalTest.Infrastructure.Framework.Instrumentation.Logging;
    using Doctus.TechnicalTest.Infrastructure.Framework.RepositoryPattern;

    public class HourService : IHourService
    {
        IRepository<Hour> hourRepository;
        ILoggerService loggerService;

        public HourService(IRepository<Hour> hourRepository, ILoggerService loggerService)
        {
            this.hourRepository = hourRepository;
            this.loggerService = loggerService;
        }

        public Hour AddHour(Hour hour)
        {
            try
            {
                var hours = hourRepository.Get(h => h.ActivityId == hour.ActivityId, null);

                if (hours.Any())
                {
                    var time = hours.Sum(h => h.Time);
                    if ((time + hour.Time) > 8) 
                    {
                        throw new ExtraHoursException();
                    }
                }

                hourRepository.Add(hour);
                hourRepository.Commit();
                return hour;
            }
            catch (ExtraHoursException)
            {
                throw;
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                throw;
            }
        }

        public IEnumerable<Hour> GetHours(int activityId)
        {
            return hourRepository.Get(a => a.ActivityId == activityId, a => a.OrderByDescending(o => o.Date));
        }
    }
}
