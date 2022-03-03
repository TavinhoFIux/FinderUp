using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finder.Core.DTO;
using Finder.Core.Model;
using Finder.Repository.FeedStockCatchRepository;
using Finder.Repository.FeedStockRepository;
using Finder.Repository.UserRepository;
using Microsoft.Extensions.Configuration;

namespace Finder.Service.FeedStockService
{
    public class FeedStockService : IFeedStockService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IFeedStockRepository _feedStockRepository;
        private readonly IFeedStockCatchRepository _feedStockCatchRepository;
        public FeedStockService(IConfiguration configuration, IUserRepository userRepository, IFeedStockRepository feedStockRepository, IFeedStockCatchRepository feedStockCatchRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _feedStockRepository = feedStockRepository;
            _feedStockCatchRepository = feedStockCatchRepository;
            
        }
        public async Task CreateFeedStock(string name, int amount, string NameUser)
        {
           var usuario = await _userRepository.GetInformationUser(NameUser);

           await _feedStockRepository.CreateFeedStock(name, amount, usuario.Id);

        }

        public List<FeedStockDTO> GetFeedStock(string name)
        {
            var feedStock = _feedStockRepository.GetFeedStock(name);
            
            return MappingFeedStockToFeedStockDTO(feedStock.ToList());

        }

        private List<FeedStockDTO> MappingFeedStockToFeedStockDTO(List<FeedStock> feedStocks)
        {
            var response =   new List<FeedStockDTO>(); 

            feedStocks.ForEach(@feedStock => response.Add(MappingFeedStock(@feedStock)));

            return response;

        }

        private FeedStockDTO MappingFeedStock(FeedStock feedStock)
        {
            var user =  _userRepository.GetInformationUser(feedStock.UserId);
            var feedStockDTO  = new FeedStockDTO 
            {
                Name = feedStock.Name,
                Quantity = feedStock.Amount,
                User = user.Name
            };

            return feedStockDTO;
        }

        public async Task UpdateFeedStock(Guid id, int amount, string userName)
        {
            var feedStockAtual = _feedStockRepository.GetFeedStockFistId(id);
            feedStockAtual.Amount = feedStockAtual.Amount - amount;
            await _feedStockRepository.UpdateFeedStock(id, feedStockAtual.Amount);
            await _feedStockCatchRepository.CreateFeedStockCatch(feedStockAtual.Name, amount, userName);

        }
    }
}