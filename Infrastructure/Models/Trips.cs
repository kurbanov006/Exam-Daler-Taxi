public class Trips
{
    public int Id { get; set; }
    public int Driver_id { get; set; }
    public int Passenger_id { get; set; }
    public string StartLocation { get; set; } = null!;
    public string EndLocation { get; set; } = null!;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public decimal Pay { get; set; }
}