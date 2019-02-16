namespace Doctus.TechnicalTest.Infrastructure.Data.DBFactory
{
    using Microsoft.EntityFrameworkCore;

    public interface IDatabaseFactory
    {
        DbContext GetDatabase();
    }
}
