using System.Collections.Generic;

namespace MauiAppTempoAgora
{
    public class Weather
    {
        public string description { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
    }

    public class Tempo
    {
        public string name { get; set; }
        public List<Weather> weather { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public double visibility { get; set; }
    }
}