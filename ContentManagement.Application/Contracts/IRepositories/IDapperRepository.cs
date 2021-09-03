using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ContentManagement.Application.Contracts.IRepositories
{
    public interface IDapperRepository
    {
        IDbConnection GetConnectionString();
    }
}
