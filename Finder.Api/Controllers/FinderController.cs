using Finder.Core.DTO;
using Finder.Service.FeedStockService;
using Finder.Service.FeesStockCatchService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinderController : ControllerBase
    {

        private readonly ILogger<FinderController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFeedStockService _feedStockService;
        private readonly IFeedStockCatchService _feedStockCatchService;

        public FinderController(ILogger<FinderController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IFeedStockService feedStockService, IFeedStockCatchService feedStockCatchService)
        {
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _feedStockService = feedStockService;
            _feedStockCatchService = feedStockCatchService;
        }

        [HttpPost]
        [Route("/rawMaterials")]
        public async Task<IActionResult> Post([FromBody] FeedStockDTO request )
        {
            await _feedStockService.CreateFeedStock(request.Name, request.Quantity, request.User );

            return Ok();
        }

        [HttpGet]
        [Route("/rawMaterials")]
        public ActionResult<List<FeedStockDTO>> Get( string name )
        {
            return  _feedStockService.GetFeedStock(name);
        }


        [HttpPut]
        [Route("/rawMaterials/feedStocks/{feedStockId:guid}/request")]
        public async Task<IActionResult> Put(Guid feedStockId, [FromBody] FeedStockDTO request)
        {
            await _feedStockService.UpdateFeedStock(feedStockId, request.Quantity, request.Name);
            return Ok();
        }

        [HttpGet]
        [Route("/rawMaterials")]
        public ActionResult<List<FeedStockCatchDTO>> GetFeedStockCatch( string user )
        {
          return _feedStockCatchService.GetListFeedStockCatch(user);
        }

        
    }
}
