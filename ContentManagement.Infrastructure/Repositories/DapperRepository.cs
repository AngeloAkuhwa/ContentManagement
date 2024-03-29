﻿using ContentManagement.Application.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ContentManagement.Infrastructure.Repositories
{
    public class DapperRepository : IDapperRepository
    {
        private IConfiguration Configuration { get; }
        private IDbConnection dbConnection;
        public DapperRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            dbConnection = new SqlConnection(Configuration.GetConnectionString("ContactConnection"));
        }
        public IDbConnection GetConnectionString()
        {
            return dbConnection;
        }
    }
}
