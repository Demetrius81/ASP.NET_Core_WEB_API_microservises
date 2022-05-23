using System;

namespace Source.Models
{
    public abstract class Metric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }
}
