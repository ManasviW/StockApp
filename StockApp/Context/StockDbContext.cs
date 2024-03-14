using Microsoft.Data.SqlClient;
using System.Data;

namespace StockApp.Context
{
    public class StockDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public StockDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public IDbConnection createConnection()=> new SqlConnection(_connectionString);
    }
}
