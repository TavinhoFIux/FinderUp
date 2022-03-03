using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Finder.Core.Model;

namespace Finder.Repository.FeedStockRepository
{
    public interface IFeedStockRepository
    {
        Task CreateFeedStock(string name, int amount, Guid userId);
        IEnumerable<FeedStock> GetFeedStock(string name);
        FeedStock GetFeedStockFistId(Guid id);
        Task UpdateFeedStock(Guid id, int amount);
    }
}