namespace Source.Models
{
    public abstract class Metric
    {
        public int Id { get; set; }
        public double Time { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Value} - {Time}";
        }
    }
}