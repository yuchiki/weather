using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace weather {

    enum Location {
        Saitama = 110010,
        Tokyo = 130010,
        Kanagawa = 140010,
        Tochigi = 090010,
        Ibaraki = 080010,
        Chiba = 120010,
        Gumma = 100010
    }

    class Program {
        static Location? Parser(string name) {
            var locationName = new Dictionary<string, Location> {
                    ["東京"] = Location.Tokyo,
                    ["tokyo"] = Location.Tokyo,
                    ["神奈川"] = Location.Kanagawa,
                    ["kanagawa"] = Location.Kanagawa,
                    ["埼玉"] = Location.Saitama,
                    ["saitama"] = Location.Saitama,
                    ["千葉"] = Location.Chiba,
                    ["chiba"] = Location.Chiba,
                    ["栃木"] = Location.Tochigi,
                    ["tochigi"] = Location.Tochigi,
                    ["群馬"] = Location.Gumma,
                    ["gumma"] = Location.Gumma,
                    ["茨城"] = Location.Ibaraki,
                    ["ibaraki"] = Location.Ibaraki
                };
            name = name.ToLower();
            return locationName.ContainsKey(name) ? locationName[name] : (Location?) null;
        }

        static void Main(string[] args) {
            if (args.Length == 0) {
                FetchShowWeather(Location.Tokyo);
            } else {
                args.Select(Parser).Where(x => x != null).Cast<Location>().Distinct().ToList().ForEach(FetchShowWeather);
            }
        }

        static dynamic Fetch(Location location) {
            var client = new WebClient();
            try {
                var s = client.DownloadString($"http://weather.livedoor.com/forecast/webservice/json/v1?city={(int)location, 0:D6}");
                return JsonConvert.DeserializeObject(s);
            } catch (WebException) {
                Console.Error.WriteLine("web error.");
                Environment.Exit(-1);
                throw;
            }

        }

        static void FetchShowWeather(Location location) {
            var json = Fetch(location);
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(json.title);
            foreach (var forcast in json.forecasts) {
                var (min, max) = (forcast.temperature.min, forcast.temperature.max);
                var rangeString = min is JObject ? $"最低気温{min.celsius}°C 最高気温{max.celsius}°C" : "";
                Console.WriteLine($" { forcast.date }: { forcast.telop } { rangeString }");
            }
            Console.WriteLine(json.description.text);
        }
    }
}
