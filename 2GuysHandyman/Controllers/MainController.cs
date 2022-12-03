using _2GuysHandyman.models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.ApiModels;

namespace _2GuysHandyman.Controllers
{
    [ApiController]
    [Route("api/")]
    public class MainController : Controller
    {
        private readonly dbContext dbContext;
        private readonly IMapper mapper;

        public MainController(dbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        #region Services

        [HttpGet]
        [Route("/services")]

        public async Task<IActionResult> GetServices()
        {
            return Ok(await dbContext.Services.ToListAsync());
        }

        [HttpGet]
        [Route("/services/{id:int}")]
        public async Task<IActionResult> GetServiceById([FromRoute] int id)
        {
            var dbService = dbContext.Services.Find(id);
            if (dbService != null)
            {
                return Ok(mapper.Map<ServicesApiModel>(dbService));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("/services")]
        public async Task<IActionResult> CreateCandidate(ServicesApiModel service)
        {
            var newService = mapper.Map<Services>(service);

            await dbContext.Services.AddAsync(newService);
            await dbContext.SaveChangesAsync();

            return Ok(newService);
        }

        [HttpDelete]
        [Route("/services/{id:int}")]
        public async Task<IActionResult> DeleteService([FromRoute] int id)
        {
            var dbService = dbContext.Services.Find(id);
            if (dbService != null)
            {
                var service = mapper.Map<ServicesApiModel>(dbService);
                dbContext.Remove(dbService);
                await dbContext.SaveChangesAsync();

                return Ok(service);
            }

            return NotFound();
        }

        [HttpPut]
        [Route("/services/{id:int}")]
        public async Task<IActionResult> UpdateService([FromRoute] int id, ServicesApiModel service)
        {
            var dbService = dbContext.Services.Find(id);
            if (dbService != null)
            {
                mapper.Map(service, dbService);

                await dbContext.SaveChangesAsync();
                return Ok(dbService);
            }

            return NotFound();
        }

        #endregion
    }
}