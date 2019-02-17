namespace Doctus.TechnicalTest.Application.Services
{
    using Doctus.TechnicalTest.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISecurityService
    {
        User AuthenticateUser(string username, string password);
    }
}
