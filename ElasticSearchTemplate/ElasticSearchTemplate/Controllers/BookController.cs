using ElasticSearchTemplate.Common.Utils;
using ElasticSearchTemplate.Domain.Entities;
using ElasticSearchTemplate.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElasticSearchTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IElasticService _elasticService;

        public BookController(IElasticService elasticService)
        {
            _elasticService = elasticService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            return Ok(await _elasticService.GetBookById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            return Ok(await _elasticService.CreateBook(book));
        }

        [HttpPost("typography")]
        public async Task<IActionResult> CreateTypography([FromBody] Typography typography)
        {
            return Ok(await _elasticService.CreateTypography(typography));
        }

        [HttpGet("typography/{id}")]
        public async Task<IActionResult> GetTypography([FromRoute] int id)
        {
            return Ok(await _elasticService.GetTypographyById(id));
        }

        [HttpGet("All")]
        public async Task<IActionResult> SearchIndex([FromQuery] string pattern)
        {
            return Ok(await _elasticService.SearchIndex(pattern));
        }

        [HttpGet("Indices/All")]
        public async Task<IActionResult> GetAllIndexes()
        {
            return Ok(await _elasticService.GetAllIndexes());
        }

        [HttpPost("Indices")]
        public async Task<IActionResult> CreateIndex([FromQuery] string indexName)
        {
            await _elasticService.CreateIndex(indexName);

            return Ok();
        }

        [HttpPost("Seed")]
        public async Task<IActionResult> Seed()
        {
            _elasticService.Seed();

            return Ok();
        }

        [HttpGet("Count")]
        public async Task<IActionResult> Count()
        {
            return Ok(await _elasticService.GetCount());
        }
    }
}
