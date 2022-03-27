using Npgsql;
using System.Data;

namespace MyFirstProject.API.Contexts
{
    public class dbContext
    {
        private readonly IConfiguration _configuration;
        public dbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_configuration.GetConnectionString("dbConnection"));

        public IDbConnection CreateMasterConnection()
                 => new NpgsqlConnection(_configuration.GetConnectionString("MasterConnection"));
    }
}
