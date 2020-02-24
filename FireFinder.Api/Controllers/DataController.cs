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
        private const int CACHETIME = 180;
        private readonly IDataProvider dataProvider;

        public DataController(IDataProvider prov)
        {
            this.dataProvider = prov ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        [ResponseCache(Duration = CACHETIME)]
        [HttpGet("current")]
        public async Task<ActionResult<RootObject>> GetCurrent()
        {
            return await dataProvider.getCurrent();
        }

        [ResponseCache(Duration = CACHETIME)]
        [HttpGet("current/{district}")]
        public async Task<ActionResult<RootObject>> GetCurrent(int district)
        {
            return await dataProvider.getCurrent(district);
        }

        [ResponseCache(Duration = CACHETIME)]
        [HttpGet("last6h")]
        public async Task<ActionResult<RootObject>> GetLast6Hours()
        {
            return await dataProvider.getLast6Hours();
        }

        [ResponseCache(Duration = CACHETIME)]
        [HttpGet("last6h/{district}")]
        public async Task<ActionResult<RootObject>> GetLast6Hours(int district)
        {
            return await dataProvider.getLast6Hours(district);
        }

        [ResponseCache(Duration = CACHETIME)]
        [HttpGet("currentd")]
        public async Task<ActionResult<RootObject>> GetCurrentDay()
        {
            return await dataProvider.getCurrentDay();
        }

        [ResponseCache(Duration = CACHETIME)]
        [HttpGet("currentd/{district}")]
        public async Task<ActionResult<RootObject>> GetCurrentDay(int district)
        {
            return await dataProvider.getCurrentDay(district);
        }

        [ResponseCache(Duration = CACHETIME)]
        [HttpGet("last2d")]
        public async Task<ActionResult<RootObject>> GetLast2Days()
        {
            return await dataProvider.getLast2Days();
        }

        [ResponseCache(Duration = CACHETIME)]
        [HttpGet("last2d/{district}")]
        public async Task<ActionResult<RootObject>> GetLast2Days(int district)
        {
            return await dataProvider.getLast2Days(district);
        }
    }
}
