using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace weather {
    class Program {
        static void Main(string[] args) =>
            (args.IsEmpty() ? new string[] { "Tokyo" } : args)
            .Select(Constants.Parser)
            .CatMaybe()
            .Distinct()
            .ForEach(x => Console.WriteLine(Fetch(x)));

        static Weather Fetch(Location location) {
            try {
                return location
                    .Call(Constants.CreateAPIURL)
                    .Call(new WebClient().DownloadString)
                    .Call(JsonConvert.DeserializeObject<Weather>);
            } catch (WebException) {
                Console.Error.WriteLine("web error.");
                Environment.Exit(-1);
                throw;
            }
        }
    }
}
