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

    static class Constants {
        public static Dictionary<string, Location?> CityNameTable = new Dictionary<string, Location?> {
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
        public static string URL = "http://weather.livedoor.com/forecast/webservice/json/v1";
        public static string CreateAPIURL(Location location) => $"{URL}?city={(int)location, 0:D6}";
        public static Location? Parser(string name) =>
            CityNameTable.GetValueOrDefault(name.ToLower(), (Location?) null);
    }
}
