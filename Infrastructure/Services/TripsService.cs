using System.Data;
using Npgsql;



public class TripService
{

    #region CreateTable
    public static void CreateTable()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CreateTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
        }


    }
    #endregion


    #region DropTable
    public static void DropTable()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.DropTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
            throw;
        }

    }
    #endregion



    #region AddTrip
    public void AddTrip(Trips trips)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandAddTrips, connection))
                {
                    cmd.Parameters.AddWithValue("@id", trips.Id);
                    cmd.Parameters.AddWithValue("@driverId", trips.Driver_id);
                    cmd.Parameters.AddWithValue("@passengerId", trips.Passenger_id);
                    cmd.Parameters.AddWithValue("@startLocation", trips.StartLocation);
                    cmd.Parameters.AddWithValue("@endLocation", trips.EndLocation);
                    cmd.Parameters.AddWithValue("@startTime", trips.StartTime);
                    cmd.Parameters.AddWithValue("@endTime", trips.EndTime);
                    cmd.Parameters.AddWithValue("@pay", trips.Pay);

                    int res = cmd.ExecuteNonQuery();
                    System.Console.WriteLine($"Add Trips: {res}");
                }
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
    #endregion


    #region GetTrip
    public Trips GetTrip(int id)
    {
        Trips trips = new Trips();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandGetTrip, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trips.Id = reader.GetInt32(0);
                            trips.Driver_id = reader.GetInt32(1);
                            trips.Passenger_id = reader.GetInt32(2);
                            trips.StartLocation = reader.GetString(3);
                            trips.EndLocation = reader.GetString(4);
                            trips.StartTime = reader.GetTimeSpan(5);
                            trips.EndTime = reader.GetTimeSpan(6);
                            trips.Pay = reader.GetDecimal(7);
                        }
                    }
                    return trips;
                }
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
    #endregion


    #region GetTrips
    public List<Trips> GetTrips()
    {
        List<Trips> myList = [];
        using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
        {
            connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandGetTrips, connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Trips trips = new Trips();
                        trips.Id = reader.GetInt32(0);
                        trips.Driver_id = reader.GetInt32(1);
                        trips.Passenger_id = reader.GetInt32(2);
                        trips.StartLocation = reader.GetString(3);
                        trips.EndLocation = reader.GetString(4);
                        trips.StartTime = reader.GetTimeSpan(5);
                        trips.EndTime = reader.GetTimeSpan(6);
                        trips.Pay = reader.GetDecimal(7);

                        myList.Add(trips);
                    }
                }
                return myList;
            }
        }
    }
    #endregion


    #region UpdateTrips
    public void UpdateTrips(Trips trips)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
        {
            connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandUpdateTrips, connection))
            {
                cmd.Parameters.AddWithValue("@id", trips.Id);
                cmd.Parameters.AddWithValue("@driverId", trips.Driver_id);
                cmd.Parameters.AddWithValue("@passengerId", trips.Passenger_id);
                cmd.Parameters.AddWithValue("@startLocation", trips.StartLocation);
                cmd.Parameters.AddWithValue("@endLocation", trips.EndLocation);
                cmd.Parameters.AddWithValue("@startTime", trips.StartTime);
                cmd.Parameters.AddWithValue("@endTime", trips.EndTime);
                cmd.Parameters.AddWithValue("@pay", trips.Pay);

                var res = cmd.ExecuteNonQuery();
                System.Console.WriteLine($"Update trip: {res}");
            }
        }
    }
    #endregion


    #region DeleteTrips
    public void DeleteTrips(int id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
        {
            connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandDeleteTrips, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                var res = cmd.ExecuteNonQuery();
                System.Console.WriteLine($"Delete trip: {res}");
            }
        }
    }
    #endregion

}








file class SqlCommand
{
    public const string CommandText = "Server=localhost; Port=5432; Database=exam_taxi; User Id=postgres; Password=12345";

    public const string CreateTable = @"CREATE TABLE trips
                                (
                                    id serial primary key,
                                    driver_id int references drivers(id),
                                    passenger_id int references passengers(id),
                                    start_location varchar(100) not null,
                                    end_location varchar(100) not null,
                                    start_time time default current_time,
                                    end_time time default current_time,
                                    pay decimal(12, 2)
                                );";
    public const string DropTable = "DROP TABLE trips";
    public const string CommandAddTrips = "INSERT INTO trips (id, driver_id, passenger_id, start_location, end_location, start_time, end_time, pay) VALUES (@id, @driverId, @passengerId, @startLocation, @endLocation, @startTime, @endTime, @pay)";
    public const string CommandGetTrip = @"SELECT * FROM trips 
                                          WHERE id = @id";
    public const string CommandGetTrips = @"SELECT * FROM trips";
    public const string CommandUpdateTrips = @"UPDATE trips SET driver_id=@driverId, passenger_id=@passengerId, start_location=@startLocation, end_location=@endLocation, start_time=@startTime, end_time=@endTime, pay=@pay
    WHERE id = @id";
    public const string CommandDeleteTrips = "DELETE FROM trips WHERE id = @id";
}