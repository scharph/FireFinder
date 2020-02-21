using System;
using System.Threading.Tasks;
using FireFinder.Logic.Models;

namespace FireFinder.Logic
{
    public interface IDataProvider
    {
        Task<RootObject> getCurrentDay(int? district = null);

        Task<RootObject> getCurrent(int? district = null);

        Task<RootObject> getLast2Days(int? district = null);

        Task<RootObject> getLast6Hours(int? district = null);
    }
}
