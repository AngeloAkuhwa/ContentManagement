using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ContentManagement.Application.Persistence.Repositories.Interfaces
{
    public interface IDapperRepository
    {
        IDbConnection GetConnectionString();
    }
}
