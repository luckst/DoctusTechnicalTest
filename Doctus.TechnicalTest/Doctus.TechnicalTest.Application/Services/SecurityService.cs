
namespace Doctus.TechnicalTest.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Doctus.TechnicalTest.Domain.Entities;
    using Doctus.TechnicalTest.Infrastructure.Framework.Instrumentation.Exceptions;
    using Doctus.TechnicalTest.Infrastructure.Framework.Instrumentation.Logging;
    using Doctus.TechnicalTest.Infrastructure.Framework.RepositoryPattern;

    public class SecurityService : ISecurityService
    {
        IRepository<User> userRepository;
        ILoggerService loggerService;

        public SecurityService(IRepository<User> userRepository, ILoggerService loggerService)
        {
            this.userRepository = userRepository;
            this.loggerService = loggerService;
        }

        public User AuthenticateUser(string username, string password)
        {
            try
            {
                var user = userRepository.Get(u => u.UserName == username && u.Password == u.Password);

                if (user is null)
                    throw new UserNotFoundException();

                return user;
            }
            catch (UserNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                throw;
            }
        }
    }
}
