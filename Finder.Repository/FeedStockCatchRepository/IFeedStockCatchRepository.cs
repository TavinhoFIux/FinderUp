using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Finder.Core.DTO;
using Finder.Core.Model;

namespace Finder.Repository.FeedStockCatchRepository
{
    public interface IFeedStockCatchRepository
    {
        Task CreateFeedStockCatch(string name, int amount, string userName);

        IEnumerable<FeedStockCatchDTO> GetListFeedStockCatch(string userName);
    }
}