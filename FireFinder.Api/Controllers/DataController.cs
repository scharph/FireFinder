using System;
using System.Threading.Tasks;
using FireFinder.Logic;
using FireFinder.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace FireFinder.Controllers
{
    [Route("api")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataProvider dataProvider;

        public DataController(IDataProvider prov)
        {
            this.dataProvider = prov ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        [ResponseCache(Duration = 60)]
        [HttpGet("current")]
        public async Task<ActionResult<RootObject>> GetCurrent()
        {
            return await dataProvider.getCurrent();
        }

        [ResponseCache(Duration = 60)]
        [HttpGet("current/{district}")]
        public async Task<ActionResult<RootObject>> GetCurrent(int district)
        {
            return await dataProvider.getCurrent(district);
        }

        [ResponseCache(Duration = 60)]
        [HttpGet("last6h")]
        public async Task<ActionResult<RootObject>> GetLast6Hours()
        {
            return await dataProvider.getLast6Hours();
        }

        [ResponseCache(Duration = 60)]
        [HttpGet("last6h/{district}")]
        public async Task<ActionResult<RootObject>> GetLast6Hours(int district)
        {
            return await dataProvider.getLast6Hours(district);
        }

        [ResponseCache(Duration = 60)]
        [HttpGet("currentd")]
        public async Task<ActionResult<RootObject>> GetCurrentDay()
        {
            return await dataProvider.getCurrentDay();
        }

        [ResponseCache(Duration = 60)]
        [HttpGet("currentd/{district}")]
        public async Task<ActionResult<RootObject>> GetCurrentDay(int district)
        {
            return await dataProvider.getCurrentDay(district);
        }

        [ResponseCache(Duration = 60)]
        [HttpGet("last2d")]
        public async Task<ActionResult<RootObject>> GetLast2Days()
        {
            return await dataProvider.getLast2Days();
        }

        [ResponseCache(Duration = 60)]
        [HttpGet("last2d/{district}")]
        public async Task<ActionResult<RootObject>> GetLast2Days(int district)
        {
            return await dataProvider.getLast2Days(district);
        }
    }
}
