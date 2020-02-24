using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using FireFinder.Logic.Models;
using Newtonsoft.Json.Linq;

namespace FireFinder.Logic
{
    public class OOELVDataProvider : IDataProvider
    {
        private static readonly string BASE_URL = "http://intranet.ooelfv.at";
    
        private static readonly string JSON_LAUFEND = $"{BASE_URL}/webext2/rss/json_laufend.txt";
        private static readonly string JSON_DAILY = $"{BASE_URL}/webext2/rss/json_taeglich.txt";
        private static readonly string JSON_6HOURS = $"{BASE_URL}/webext2/rss/json_6stunden.txt";
        private static readonly string JSON_2DAYS = $"{BASE_URL}/webext2/rss/json_2tage.txt";

        HttpClient client;

        public OOELVDataProvider()
        {
            client = new HttpClient();
        }

        public async Task<RootObject> getCurrent(int? district = null)
        {
            var prodResp = await client.GetAsync(JSON_LAUFEND);
            var content = await prodResp.Content.ReadAsStringAsync();

            JObject rss = JObject.Parse(content);
            return district != null ? getParsedRootObject(rss, district) : getParsedRootObject(rss);
        }

        public async Task<RootObject> getCurrentDay(int? district = null)
        {
            var prodResp = await client.GetAsync(JSON_DAILY);
            var content = await prodResp.Content.ReadAsStringAsync();

            JObject rss = JObject.Parse(content);
            return district != null ? getParsedRootObject(rss, district) : getParsedRootObject(rss);
        }

        public async Task<RootObject> getLast6Hours(int? district = null)
        {
            var prodResp = await client.GetAsync(JSON_6HOURS);
            var content = await prodResp.Content.ReadAsStringAsync();

            JObject rss = JObject.Parse(content);
            return district != null ? getParsedRootObject(rss, district) : getParsedRootObject(rss);
        }

        public async Task<RootObject> getLast2Days(int? district = null)
        {
            var prodResp = await client.GetAsync(JSON_2DAYS);
            var content = await prodResp.Content.ReadAsStringAsync();

            JObject rss = JObject.Parse(content);
            return district != null ? getParsedRootObject(rss, district) : getParsedRootObject(rss);
        }

        private RootObject getParsedRootObject(JObject rss, int? district = null)
        {
            RootObject ro = new RootObject();

            ro.Title = rss["title"].ToString();
            ro.LastRefresh = DateTime.Now;
            ro.Published = DateTime.Parse(rss["pubDate"].ToString()).ToLocalTime();
            ro.Operations = new List<Operation>();

            if (rss["einsaetze"] != null)
            {
                foreach (var es in rss["einsaetze"].Values())
                {
                    var einsatz = es["einsatz"];

                    if (district == Convert.ToInt16(einsatz["bezirk"]["id"].ToString()) || district == null)
                    {
                        Operation e = new Operation
                        {
                            Alarmlevel = Convert.ToInt16(einsatz["alarmstufe"].ToString()),
                            Operationtype = einsatz["einsatzart"].ToString(),
                            Id = einsatz["num1"].ToString(),
                            Starttime = einsatz["startzeit"].ToString() != string.Empty ? DateTime.Parse(einsatz["startzeit"].ToString()) : DateTime.MinValue,
                            Endtime = einsatz["inzeit"].ToString() != string.Empty ? new Nullable<DateTime>(DateTime.Parse(einsatz["inzeit"].ToString())) : null,
                            State = einsatz["status"].ToString(),
                            Units = new List<Unit>()
                        };

                        var eTyp = einsatz["einsatztyp"];
                        var eSubtyp = einsatz["einsatzsubtyp"];
                        e.Type = new Models.Type
                        {
                            Id = eTyp["id"].ToString(),
                            Name = eTyp["text"].ToString(),
                            SubId = eSubtyp["id"].ToString(),
                            SubName = eSubtyp["text"].ToString()
                        };

                        var coor = einsatz["wgs84"];
                        var addr = einsatz["adresse"];

                        var lat = Convert.ToDouble(coor["lat"].ToString());
                        var lng = Convert.ToDouble(coor["lng"].ToString());
                        e.Destination = new Destination
                        {
                            Title = einsatz["einsatzort"].ToString(),
                            Address = addr["default"].ToString(),
                            Area = addr["earea"].ToString(),
                            Number = addr["estnum"].ToString(),
                            City = addr["emun"].ToString(),
                            Addon = addr["ecompl"].ToString(),
                            District = new District
                            {
                                Id = Convert.ToInt16(einsatz["bezirk"]["id"].ToString()),
                                Name = einsatz["bezirk"]["text"].ToString()
                            },
                            Geo = new Geo
                            {
                                Latitude = lat,
                                Longitude = lng,
                                MapsLink = $"https://www.google.com/maps/search/?api=1&query={lat.ToString("G", CultureInfo.InvariantCulture)},{lng.ToString("G", CultureInfo.InvariantCulture)}",
                                Type = "WGS84"
                            }
                            

                        };

                        foreach (var fwdata in einsatz["feuerwehrenarray"].Values())
                        {
                            e.Units.Add(new Unit { Name = fwdata["fwname"].ToString(), Id = fwdata["fwnr"].ToString() });
                        }

                        ro.Operations.Add(e);
                    }
                }
            }

            return ro;
        }
    }
}
