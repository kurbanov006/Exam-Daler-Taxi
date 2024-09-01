public class Rating
{
    public int Id { get; set; }
    public int Driver_id { get; set; }
    public int Trips_id { get; set; }
    public int Passenger_id { get; set; }
    public int Ratings { get; set; }
    public string Comment { get; set; } = null!;
}