using _2GuysHandyman.ApiModels;
using _2GuysHandyman.models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
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
        [HttpGet, Authorize]
        [Route("/services")]

        public async Task<IActionResult> GetServices()
        {
            if(User?.Identity?.Name != "msargsyan2002@outlook.com")
            {
                return Forbid();
            }

            return Ok(await dbContext.Services.ToListAsync());
        }

        [HttpGet, Authorize]
        [Route("/services/{id:int}")]
        public async Task<IActionResult> GetServiceById([FromRoute] int id)
        {
            if (User?.Identity?.Name != "msargsyan2002@outlook.com")
            {
                return Forbid();
            }

            var dbService = dbContext.Services.Find(id);
            if (dbService != null)
            {
                return Ok(mapper.Map<ServicesApiModel>(dbService));
            }

            return NotFound();
        }

        [HttpPost, Authorize]
        [Route("/services")]
        public async Task<IActionResult> CreateCandidate(ServicesApiModel service)
        {
            if (User?.Identity?.Name != "msargsyan2002@outlook.com")
            {
                return Forbid();
            }

            var newService = mapper.Map<Services>(service);

            await dbContext.Services.AddAsync(newService);
            await dbContext.SaveChangesAsync();

            return Ok(newService);
        }

        [HttpDelete, Authorize]
        [Route("/services/{id:int}")]
        public async Task<IActionResult> DeleteService([FromRoute] int id)
        {
            if (User?.Identity?.Name != "msargsyan2002@outlook.com")
            {
                return Forbid();
            }

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

        [HttpPut, Authorize]
        [Route("/services/{id:int}")]
        public async Task<IActionResult> UpdateService([FromRoute] int id, ServicesApiModel service)
        {
            if (User?.Identity?.Name != "msargsyan2002@outlook.com")
            {
                return Forbid();
            }

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

        #region Orders

        [HttpGet, Authorize]
        [Route("/orders")]

        public async Task<IActionResult> GetOrders()
        {
            if (User?.Identity?.Name != "msargsyan2002@outlook.com")
            {
                return Forbid();
            }

            return Ok(await dbContext.Orders.ToListAsync());
        }

        [HttpGet, Authorize]
        [Route("/orders/{id:int}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            var dbOrder = dbContext.Orders.Find(id); //.Where(o => o.Id == id).Include(o => o.User);
            if (dbOrder == null)
            {
                return NotFound();
            }

            if (User?.Identity?.Name != "msargsyan2002@outlook.com" || User?.Identity?.Name != dbOrder.User.Email)
            {
                return Forbid();
            }

            return Ok(mapper.Map<ServicesApiModel>(dbOrder));

        }

        [HttpPost, Authorize]
        [Route("/orders")]
        public async Task<IActionResult> CreateOrder(OrdersApiModel order)
        {
            var newOrder = mapper.Map<Orders>(order);

            newOrder.User = dbContext.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            newOrder.Price = dbContext.Services.FirstOrDefault(s => s.Id == order.ServiceId).Price;
            
            await dbContext.Orders.AddAsync(newOrder);
            await dbContext.SaveChangesAsync();

            return Ok(newOrder);
        }

        [HttpDelete, Authorize]
        [Route("/orders/{id:int}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var dbOrder = dbContext.Orders.Find(id);
            if (dbOrder == null)
            {
                 return NotFound();
            }

            if (User?.Identity?.Name != "msargsyan2002@outlook.com" || User?.Identity?.Name != dbOrder.User.Email)
            {
                return Forbid();
            }

            var order = mapper.Map<OrdersApiModel>(dbOrder);
            dbContext.Remove(dbOrder);
            await dbContext.SaveChangesAsync();

            return Ok(order);

        }

        [HttpPut, Authorize]
        [Route("/services/{id:int}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, OrdersApiModel order)
        {
            var dbOrder = dbContext.Orders.Find(id);
            if (dbOrder == null)
            {
               return NotFound();
            }

            if (User?.Identity?.Name != "msargsyan2002@outlook.com" || User?.Identity?.Name != dbOrder.User.Email)
            {
                return Forbid();
            }

            mapper.Map(order, dbOrder);

            await dbContext.SaveChangesAsync();
            return Ok(dbOrder);
        }

        #endregion
    }
}