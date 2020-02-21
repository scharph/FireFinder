using System;
using System.Collections.Generic;
using FireFinder.Logic.Models;

namespace FireFinder.Logic
{
    public interface IDistrictProvider
    {
        List<District> GetDistricts();
    }
}
