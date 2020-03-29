namespace MusicBAWT.Objects
{
    public class Weather
    {
        public float Temperature;
        public string Location;
        public float Humidity;
        public string Description;
    }

    public class IntermediateWeather
    {   
        // local class definitions
        public class Main
        {
            public float temp { get; set; }
            public float humidity { get; set; }

            public float getTempCelsius()
            {
                return this.temp - (float)272.15;
            }
        }
        
        public class Context
        {
            private string _description;
            public string description
            {
                set { _description = char.ToUpper(value[0]) + value.Substring(1); }
                get { return _description; }
            }
        }

        public string name { get; set; }
        public Main main { get; set; }
        public Context[] weather { get; set; }

        public static implicit operator Weather(IntermediateWeather intermediateWeather)
        {
            Weather weather = new Weather();
            weather.Description = intermediateWeather.weather[0].description;
            weather.Humidity = intermediateWeather.main.humidity;
            weather.Temperature = intermediateWeather.main.getTempCelsius();
            weather.Location = intermediateWeather.name;
            return weather;
        }
    }
}