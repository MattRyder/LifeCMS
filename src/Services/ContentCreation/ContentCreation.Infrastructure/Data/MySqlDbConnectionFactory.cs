using System.Data;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using MySqlConnector;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Data
{
    public class MySqlDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public MySqlDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
