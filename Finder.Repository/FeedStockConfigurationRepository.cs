using Dapper;
using Finder.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Finder.Repository
{
    public class FeedStockConfigurationRepository : BaseRepository
    {
        public FeedStockConfigurationRepository(IConfiguration configuration) : base(configuration)
        {
        }        
    }
}
