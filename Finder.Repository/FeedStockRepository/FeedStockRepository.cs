using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Finder.Core.Model;
using Microsoft.Extensions.Configuration;

namespace Finder.Repository.FeedStockRepository
{
    public class FeedStockRepository : BaseRepository, IFeedStockRepository
    {
        private readonly IDbConnection dbConnection;

        public FeedStockRepository(IConfiguration configuration): base(configuration) 
        {
                dbConnection = new SqlConnection(_connectionString);
        }
        public async Task CreateFeedStock(string name, int amount, Guid userId)
        {
        
            var feedStock = new FeedStock() { Name = name, Amount = amount, UserId = userId};

            string query = "INSERT INTO FeedStock ( Name, Amount , UserId) VALUES ( @Name, @Amount, @UserId)";

            await dbConnection.ExecuteAsync(query, feedStock);
        }

        public IEnumerable<FeedStock> GetFeedStock(string name)
        {
            
            string query = "SELECT * FROM FeedStock WHERE Name Like @Name";

            return dbConnection.Query<FeedStock>(query, new { @Name = name + "%"});
        }

        public FeedStock GetFeedStockFistId(Guid id)
        {
            string query = "SELECT * FROM FeedStock WHERE Id =  @Id";

            return dbConnection.QueryFirst<FeedStock>(query, new { @Id = id});
        }

        public async Task UpdateFeedStock(Guid id, int amount)
        {
            string query = "UPDATE FeedStock SET Amount = @Amount WHERE Id = @Id";

            await dbConnection.ExecuteAsync(query, new { @Amount = amount, @Id = id });

        }
    }
}