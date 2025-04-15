using Newtonsoft.Json;
using System.Collections.Generic;

public class City
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public Coord coord { get; set; } = new Coord();
    public string country { get; set; } = string.Empty;
    public int population { get; set; }
    public int timezone { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }
}

public class Clouds
{
    public int all { get; set; }
}

public class Coord
{
    public double lat { get; set; }
    public double lon { get; set; }
}

// ✅ Renamed "List" class to avoid conflict with C# List<>
public class WeatherForecastItem
{
    public int dt { get; set; }
    public Main main { get; set; } = new Main();
    public List<Weather> weather { get; set; } = new List<Weather>();
    public Clouds clouds { get; set; } = new Clouds();
    public Wind wind { get; set; } = new Wind();
    public int visibility { get; set; }
    public double pop { get; set; }
    public Sys sys { get; set; } = new Sys();
    public string dt_txt { get; set; } = string.Empty;
    public Rain rain { get; set; } = new Rain();
}

public class Main
{
    public double temp { get; set; }
    public double feels_like { get; set; }
    public double temp_min { get; set; }
    public double temp_max { get; set; }
    public int pressure { get; set; }
    public int sea_level { get; set; }
    public int grnd_level { get; set; }
    public int humidity { get; set; }
    public double temp_kf { get; set; }
}

public class Rain
{
    [JsonProperty("3h")]
    public double _3h { get; set; }
}

public class WeatherDescription
{
    public required string description { get; set; }
}

public class WeatherData
{
    public required Main main { get; set; }
    public required List<WeatherDescription> weather { get; set; }
    public string name { get; set; } = string.Empty;
}

public class WeatherDataItem
{
    public long dt { get; set; }
    public Main main { get; set; }  // ✅ Changed WeatherMain → Main
    public List<WeatherDescription> weather { get; set; }
}

public class Root
{
    public City city { get; set; } = new City();
    public List<WeatherForecastItem> list { get; set; } = new List<WeatherForecastItem>(); // ✅ Changed List → WeatherForecastItem
}

public class Sys
{
    public string pod { get; set; } = string.Empty;
}

public class Weather
{
    public int id { get; set; }
    public string main { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string icon { get; set; } = string.Empty;
}

public class Wind
{
    public double speed { get; set; }
    public int deg { get; set; }
    public double gust { get; set; }
}
