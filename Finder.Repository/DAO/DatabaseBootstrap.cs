using Dapper;
using Finder.Core.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finder.Repository.DAO
{
    public class DatabaseBootstrap
    {
        private readonly string _connectionString;

        public DatabaseBootstrap(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task Setup()
        {            
            using IDbConnection dbConnection = new SqlConnection(_connectionString);

            await CreateTables(dbConnection);
            await SeedData(dbConnection);    
        }

        private async Task SeedData(IDbConnection dbConnection)
        {
                var usuario = new List<Usuario>()
            {
                new Usuario() { Id = Guid.NewGuid(), Name = "John Doe", Email="hdinizribeiro@gmail.com"  },
                new Usuario() { Id = Guid.NewGuid(), Name = "Huguinho", Email="huguinho@domain.com"   },
                new Usuario() { Id = Guid.NewGuid(), Name = "Luis", Email="oluisotavio2@hotmail.com" },
                
            };
        
          
            var feedStock = new List<FeedStock>()
            {
                new FeedStock() { Id = Guid.NewGuid(), Name = "Açuca", Amount = 10,  UserId = usuario.First(c => c.Name == "John Doe").Id },
                new FeedStock() { Id = Guid.NewGuid(), Name = "Agua",  Amount = 10, UserId = usuario.First(c => c.Name == "John Doe").Id  }
            };

    

            await dbConnection.ExecuteAsync("USE [Finder]; delete from FeedStock; delete from Usuario; delete from FeedStockRecord");

            await dbConnection.ExecuteAsync("USE [Finder]; INSERT INTO Usuario (Id, Name, Email ) VALUES (@Id, @Name, @Email )", usuario);

            await dbConnection.ExecuteAsync("USE [Finder]; INSERT INTO FeedStock (Id, Name, Amount , UserId) VALUES (@Id, @Name, @Amount, @UserId)", feedStock);
        }

        private async Task CreateTables(IDbConnection dbConnection)
        {
            string createDatabseQuery = @"
            IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Finder')
            BEGIN
                CREATE DATABASE [Finder]
            END";
            
            string createTablesQuery =
            @"USE [Finder]
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Usuario' and xtype='U')
            BEGIN
                CREATE TABLE [dbo].[Usuario]
                (
                    [Id] UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL PRIMARY KEY,
                    [Name] NVARCHAR (150) NOT NULL,
                    [Email] NVARCHAR (150) NOT NULL,
                );
            END

             IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FeedStockRecord' and xtype='U')
            BEGIN
                CREATE TABLE [dbo].[FeedStockRecord]
                (	
                    [Id] UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL PRIMARY KEY,
                    [AmountCatch] INT NULL,
                    [Name] NVARCHAR (150) NOT NULL,
                    [UserName] NVARCHAR (150) NOT NULL,
                    [DateCreate] datetime DEFAULT getdate(),
                );
            END


            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FeedStock' and xtype='U')
            BEGIN
                CREATE TABLE [dbo].[FeedStock]
                (	
                    [Id] UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL PRIMARY KEY,
                    [Amount] INT NULL,
                    [Name] NVARCHAR (150) NOT NULL,
                    [UserId] UNIQUEIDENTIFIER NOT NULL,
                    CONSTRAINT [FK_FeedStock_Usuario] FOREIGN KEY ([UserId]) REFERENCES [Usuario]([Id]),
                );
            END;";

            await dbConnection.ExecuteAsync(createDatabseQuery);
            await dbConnection.ExecuteAsync(createTablesQuery);
        }
    }
}
