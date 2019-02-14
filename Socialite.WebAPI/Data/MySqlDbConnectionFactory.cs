using System.Data;
using MySql.Data.MySqlClient;

class MySqlDbConnectionFactory : IDbConnectionFactory
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