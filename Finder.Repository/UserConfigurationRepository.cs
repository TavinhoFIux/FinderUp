using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Finder.Repository
{
    public class UserConfigurationRepository : BaseRepository
    {
        public UserConfigurationRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
