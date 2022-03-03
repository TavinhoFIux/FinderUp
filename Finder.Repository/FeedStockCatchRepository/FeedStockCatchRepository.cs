using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Finder.Core.DTO;
using Finder.Core.Model;
using Microsoft.Extensions.Configuration;

namespace Finder.Repository.FeedStockCatchRepository
{
    public class FeedStockCatchRepository : BaseRepository,  IFeedStockCatchRepository
    {
         private readonly IDbConnection dbConnection;
        public FeedStockCatchRepository(IConfiguration configuration) : base(configuration)
        {
                dbConnection = new SqlConnection(_connectionString);
        }

        public async Task CreateFeedStockCatch(string name, int amount, string userName)
        {
                    
            var feedStock = new FeedStockCatch() { Name = name, AmountCatch = amount, UserName = userName, DateCreate = new DateTime()};

            string query = "INSERT INTO FeedStockCatch ( Name, AmountCatch , UserName, DateCreate) VALUES ( @Name, @AmountCatch, @UserName, @DateCreate)";

            await dbConnection.ExecuteAsync(query, feedStock);
        }

        public IEnumerable<FeedStockCatchDTO> GetListFeedStockCatch(string userName)
        {
            string query = "select SUM(AmountCatch) as quantidade, Name as name, (select DISTINCT  UserName from FeedStockRecord where  UserName = @UserName) as userName from FeedStockRecord where UserName = @UserName group by Name ";

            return dbConnection.Query<FeedStockCatchDTO>(query, new { @UserName = userName});
        }
    }
}