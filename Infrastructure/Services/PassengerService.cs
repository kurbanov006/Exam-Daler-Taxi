using Npgsql;



public class PassengerService
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
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
    #endregion


    #region AddPassenger

    public void AddPassenger(Passenger passenger)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandAddPassenger, connection))
                {
                    cmd.Parameters.AddWithValue("@id", passenger.Id);
                    cmd.Parameters.AddWithValue("@fName", passenger.FirstName);
                    cmd.Parameters.AddWithValue("@lName", passenger.LastName);
                    cmd.Parameters.AddWithValue("@pNumber", passenger.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", passenger.Email);

                    int res = cmd.ExecuteNonQuery();
                    System.Console.WriteLine($"Add Passenger: {res}");
                }
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }

    }
    #endregion


    #region GetPassenger
    public Passenger GetPassenger(int id)
    {
        try
        {
            Passenger passenger = new Passenger();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandGetPassenger, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            passenger.Id = reader.GetInt32(0);
                            passenger.FirstName = reader.GetString(1);
                            passenger.LastName = reader.GetString(2);
                            passenger.PhoneNumber = reader.GetString(3);
                            passenger.Email = reader.GetString(4);

                        }
                        return passenger;
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


    #region GetPassengers
    public List<Passenger> GetPassengers()
    {
        try
        {
            List<Passenger> myList = [];
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandGetPassengers, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Passenger passenger = new Passenger();
                            passenger.Id = reader.GetInt32(0);
                            passenger.FirstName = reader.GetString(1);
                            passenger.LastName = reader.GetString(2);
                            passenger.PhoneNumber = reader.GetString(3);
                            passenger.Email = reader.GetString(4);

                            myList.Add(passenger);
                        }
                    }
                    return myList;
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


    #region UpdatePassenger
    public void UpdatePassenger(Passenger passenger)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandUpdatePassenger, connection))
                {
                    cmd.Parameters.AddWithValue("@id", passenger.Id);
                    cmd.Parameters.AddWithValue("@fName", passenger.FirstName);
                    cmd.Parameters.AddWithValue("@lName", passenger.LastName);
                    cmd.Parameters.AddWithValue("@pNumber", passenger.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", passenger.Email);

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


    #region DeletePassenger
    public void DeletePassenger(int id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.CommandText))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandDeletePassenger, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

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
}



file class SqlCommand
{
    public const string CommandText = "Server=localhost; Port=5432; Database=exam_taxi; User Id=postgres; Password=12345";
    public const string CommandCreateTable = @"CREATE TABLE passengers
                                                (
                                                    id serial primary key,
                                                    first_name varchar(100) not null,
                                                    last_name varchar(100) not null,
                                                    phone_number varchar(20) not null,
                                                    email varchar(100) not null
                                                );";
    public const string CommandAddPassenger = "INSERT INTO passengers(id, first_name, last_name, phone_number, email) VALUES (@id, @fName, @lName, @pNumber, @email)";

    public const string CommandGetPassenger = "SELECT * FROM passengers where id = @id";
    public const string CommandGetPassengers = "SELECT * FROM passengers";
    public const string CommandUpdatePassenger = @"UPDATE passengers SET first_name=@fName, last_name=@lName, phone_number=@pNumber, email=@email
                                                   WHERE id = @id";
    public const string CommandDeletePassenger = @"DELETE FROM passengers 
                                                  WHERE id = @id";
}