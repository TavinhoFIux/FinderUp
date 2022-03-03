using System.Collections.Generic;
using Finder.Core.DTO;

namespace Finder.Service.FeesStockCatchService
{
    public interface IFeedStockCatchService
    {
        List<FeedStockCatchDTO> GetListFeedStockCatch(string userName);
    }
}