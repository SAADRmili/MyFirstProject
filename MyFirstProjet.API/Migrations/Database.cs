using Dapper;
using MyFirstProject.API.Contexts;

namespace MyFirstProject.API.Migrations
{
    public class Database
    {
        private readonly dbContext _dbContext;

        public Database(dbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }
        public void CreateDatabase(string dbName)
        {
            var query = "SELECT * FROM pg_database WHERE datname = @name";
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);

            using (var connection = _dbContext.CreateMasterConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {dbName}");
            }
        }
    }
}
