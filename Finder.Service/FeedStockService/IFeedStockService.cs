using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Finder.Core.DTO;
using Finder.Core.Model;

namespace Finder.Service.FeedStockService
{
    public interface IFeedStockService
    {
        Task CreateFeedStock(string name, int amount, string NameUser);
        Task UpdateFeedStock(Guid id, int amount, string userName);
        List<FeedStockDTO> GetFeedStock(string name);
    }
}