using ContentManagement.Application.Contracts.IRepositories;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ContentManagement.Persistence.Repositories
{
    public class DapperRepository : IDapperRepository
    {
        private IConfiguration Configuration { get; }
        private IDbConnection dbConnection;
        public DapperRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            dbConnection = new SqlConnection(Configuration.GetConnectionString("ContactConnection"));
        }
        public IDbConnection GetConnectionString()
        {
            return dbConnection;
        }
    }
}
