using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Finder.Core.Model;
using Microsoft.Extensions.Configuration;

namespace Finder.Repository.UserRepository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
         private readonly IDbConnection dbConnection;
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
            dbConnection = new SqlConnection(_connectionString);
        }

        public async Task<Usuario> GetInformationUser(string name)
        {
           string query = "SELECT * FROM Usuario WHERE Name = @UserName";

            return  await dbConnection.QueryFirstAsync<Usuario>(query, new { @UserName = name});
        }

        public Usuario GetInformationUser(Guid id)
        {
              string query = "SELECT * FROM Usuario WHERE Id = @Id";

            return   dbConnection.QueryFirst<Usuario>(query, new { @Id = id});
        }
    }
}