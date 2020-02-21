using System;
using System.Collections.Generic;
using FireFinder.Logic.Models;

namespace FireFinder.Logic
{
    public class DistrictProvider : IDistrictProvider
    {
        public List<District> GetDistricts()
        {
            List<District> districts = new List<District>();

            districts.Add(new District { Id = 0, Name = "Braunau" });
            districts.Add(new District { Id = 1, Name = "Eferding" });
            districts.Add(new District { Id = 2, Name = "Freistadt" });
            districts.Add(new District { Id = 3, Name = "Gmunden" });
            districts.Add(new District { Id = 4, Name = "Grieskirchen" });
            districts.Add(new District { Id = 5, Name = "Kirchdorf" });
            districts.Add(new District { Id = 6, Name = "Linz-Land" });
            districts.Add(new District { Id = 7, Name = "Perg" });
            districts.Add(new District { Id = 8, Name = "Ried" });
            districts.Add(new District { Id = 9, Name = "Rohrbach" });
            districts.Add(new District { Id = 10, Name = "Schärding" });
            districts.Add(new District { Id = 11, Name = "Steyr-Land" });
            districts.Add(new District { Id = 12, Name = "Urfahr-Umgebung" });
            districts.Add(new District { Id = 13, Name = "Vöcklabruck" });
            districts.Add(new District { Id = 14, Name = "Wels-Land" });
            districts.Add(new District { Id = 15, Name = "Steyr-Stadt" });
            districts.Add(new District { Id = 16, Name = "Wels-Stadt" });
            districts.Add(new District { Id = 17, Name = "Linz-Stadt" });

            return districts;
        }
    }
}

