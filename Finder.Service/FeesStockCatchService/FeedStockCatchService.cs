using System.Collections.Generic;
using System.Linq;
using Finder.Core.DTO;
using Finder.Repository.FeedStockCatchRepository;
using Microsoft.Extensions.Configuration;

namespace Finder.Service.FeesStockCatchService
{
    public class FeedStockCatchService : IFeedStockCatchService
    {
        private readonly IFeedStockCatchRepository _feedStockCatchRepository;

        private readonly IConfiguration _configuration;
        public FeedStockCatchService(IFeedStockCatchRepository feedStockCatchRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _feedStockCatchRepository = feedStockCatchRepository;
        }
        public List<FeedStockCatchDTO> GetListFeedStockCatch(string userName)
        {
            return _feedStockCatchRepository.GetListFeedStockCatch(userName).ToList();
        }
    }
}