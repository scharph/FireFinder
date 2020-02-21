using System;
using System.Collections.Generic;
using FireFinder.Logic;
using FireFinder.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace FireFinder.API.Controllers
{
    [Route("api/district")]
    [ApiController]
    public class DistrictController
    {
        IDistrictProvider districtProvider;

        public DistrictController(IDistrictProvider provider)
        {
            this.districtProvider = provider;
        }

        [HttpGet]
        public IEnumerable<District> GetDistricts()
        {
            return districtProvider.GetDistricts();
        }
    }
}
