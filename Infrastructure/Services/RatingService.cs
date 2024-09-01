using Npgsql;






public class RatingService
{

    #region CreateTable
    public static void CreateTable()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandCreateTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine($"Error: {ex}");
            throw;
        }
    }
    #endregion

    #region DeleteTable
    public static void DeleteTable()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
        {
            connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandDropTable, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
    #endregion



    #region AddRating
    public void AddRating(Rating rating)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandAddRating, connection))
                {
                    cmd.Parameters.AddWithValue("@id", rating.Id);
                    cmd.Parameters.AddWithValue("@driverId", rating.Driver_id);
                    cmd.Parameters.AddWithValue("@tripId", rating.Trips_id);
                    cmd.Parameters.AddWithValue("@passengerId", rating.Passenger_id);
                    cmd.Parameters.AddWithValue("@rating", rating.Ratings);
                    cmd.Parameters.AddWithValue("@comment", rating.Comment);

                    int res = cmd.ExecuteNonQuery();
                    System.Console.WriteLine($"Add Rating: {res}");
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


    #region GetRating
    public Rating GetRating(int id)
    {
        try
        {
            Rating rating = new Rating();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandGetRating, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rating.Id = reader.GetInt32(0);
                            rating.Driver_id = reader.GetInt32(1);
                            rating.Trips_id = reader.GetInt32(2);
                            rating.Passenger_id = reader.GetInt32(3);
                            rating.Ratings = reader.GetInt32(4);
                            rating.Comment = reader.GetString(5);

                        }
                        return rating;
                    }
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


    #region GetRatings
    public List<Rating> GetRatings()
    {
        try
        {
            List<Rating> myList = [];
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandGetRatings, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rating rating = new Rating();
                            rating.Id = reader.GetInt32(0);
                            rating.Driver_id = reader.GetInt32(1);
                            rating.Trips_id = reader.GetInt32(2);
                            rating.Passenger_id = reader.GetInt32(3);
                            rating.Ratings = reader.GetInt32(4);
                            rating.Comment = reader.GetString(5);

                            myList.Add(rating);
                        }
                        return myList;
                    }
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


    #region UpdateRating
    public void UpdateRating(Rating rating)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandUpdateRating, connection))
                {
                    cmd.Parameters.AddWithValue("@id", rating.Id);
                    cmd.Parameters.AddWithValue("@driverId", rating.Driver_id);
                    cmd.Parameters.AddWithValue("@tripId", rating.Trips_id);
                    cmd.Parameters.AddWithValue("@passengerId", rating.Passenger_id);
                    cmd.Parameters.AddWithValue("@rating", rating.Ratings);
                    cmd.Parameters.AddWithValue("@comment", rating.Comment);

                    int res = cmd.ExecuteNonQuery();
                    System.Console.WriteLine($"Add Rating: {res}");
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


    #region DeleteRating
    public void DeleteRating(int id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandDelteRating, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int res = cmd.ExecuteNonQuery();
                    System.Console.WriteLine($"Delete rating: {res}");
                }
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine($"Error: {ex}");
            throw;
        }

    }
    #endregion

}




file class SqlCommand
{
    public const string CommandText = "Server=localhost; Port=5432; Database=exam_taxi; User Id=postgres; Password=12345";

    public const string CommandCreateTable = @"create table ratings
                                            ( 
                                                id serial primary key,
                                                driver_id int references drivers(id),
                                                trip_id int references trips(id),
                                                passenger_id int references passengers(id),
                                                ratings int,
                                                comment varchar(255) not null
                                            );";
    public const string CommandDropTable = "Drop Table ratings";
    public const string CommandAddRating = @"INSERT INTO ratings (id, driver_id, trip_id, passenger_id, ratings, comment) VALUES
                                            (@id, @driverId, @tripId, @passengerId, @rating, @comment)";

    public const string CommandGetRating = "SELECT * FROM ratings WHERE id = @id";
    public const string CommandGetRatings = "SELECT * FROM ratings";
    public const string CommandUpdateRating = "UPDATE ratings SET driver_id=@driverId, trip_id=@tripId, passenger_id=@passengerId, ratings=@rating, comment=@comment WHERE id = @id";

    public const string CommandDelteRating = "DELETE FROM ratings WHERE id = @id";
}