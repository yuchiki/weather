using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace weather {
    class Weather {
        [JsonProperty("title")]
        public string Title { get; private set; }

        [JsonProperty("forecasts")]
        public List<Forecast> Forecasts { get; private set; }

        [JsonProperty("description")]
        public Description Description { get; private set; }

        public override string ToString() =>
            $@"--------------------------------------------
{Title}
{Forecasts.Select(x=>$"{x}").Call(x=>String.Join("\n", x))}
{Description}";
    }

    class Forecast {
        [JsonProperty("date")]
        public string Date { get; private set; }

        [JsonProperty("telop")]
        public string Telop { get; private set; }

        [JsonProperty("temperature")]
        public Temperature Temperature { get; private set; }

        public override string ToString() =>
            $" { Date }: { Temperature } { Telop }";
    }

    class Description {
        [JsonProperty("text")]
        public string Text { get; private set; }
        public override string ToString() => Text;
    }

    class Temperature {
        [JsonProperty("min")]
        public SingleTemperature Min { get; private set; }

        [JsonProperty("max")]
        public SingleTemperature Max { get; private set; }

        public override string ToString() =>
            $"最低気温{Min?.Celsius, 3 }°C 最高気温{Max?.Celsius, 3 }°C";
    }

    class SingleTemperature {
        [JsonProperty("celsius")]
        public int Celsius { get; private set; }
    }
}
