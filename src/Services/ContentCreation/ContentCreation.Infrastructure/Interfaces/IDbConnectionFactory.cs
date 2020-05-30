using System.Data;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
