using System.Data;
using System.Reflection.Metadata;
using Npgsql;




public class DriverService
{
    #region CreateDatabase
    public static void CreateDatabase()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionCreateDatabase))
        {
            connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandTextCreateDatabase, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
    #endregion


    #region CreateTable
    public static void CreateTable()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionDatabase))
        {
            connection.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandTextCreateTable, connection))
            {
                int res = cmd.ExecuteNonQuery();
                System.Console.WriteLine($"Create table: {res}");
            }
        }
    }
    #endregion



    #region AddDriver
    public void AddDriver(Driver driver)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionDatabase))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandTextAddDriver, connection))
                {
                    cmd.Parameters.AddWithValue("@id", driver.Id);
                    cmd.Parameters.AddWithValue("@fName", driver.Firstname);
                    cmd.Parameters.AddWithValue("@lName", driver.LastName);
                    cmd.Parameters.AddWithValue("@pNumber", driver.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", driver.Email);
                    cmd.Parameters.AddWithValue("@carModel", driver.CarModel);
                    cmd.Parameters.AddWithValue("@carColor", driver.CarColor);

                    int res = cmd.ExecuteNonQuery();
                    System.Console.WriteLine($"Inser Driver: {res}");
                }
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
    #endregion


    #region GetDriver
    public Driver GetDriver(int id)
    {
        try
        {
            Driver driver = new Driver();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionDatabase))
            {

                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandTextGetDriver, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            driver.Id = reader.GetInt32(0);
                            driver.Firstname = reader.GetString(1);
                            driver.LastName = reader.GetString(2);
                            driver.PhoneNumber = reader.GetString(3);
                            driver.Email = reader.GetString(4);
                            driver.CarModel = reader.GetString(5);
                            driver.CarColor = reader.GetString(6);

                        }
                        return driver;

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


    #region GetDrivers
    public List<Driver> GetDrivers()
    {
        try
        {
            List<Driver> myList = [];
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionDatabase))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandTextGetDrivers, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Driver driver = new Driver();
                            driver.Id = reader.GetInt32(0);
                            driver.Firstname = reader.GetString(1);
                            driver.LastName = reader.GetString(2);
                            driver.PhoneNumber = reader.GetString(3);
                            driver.Email = reader.GetString(4);
                            driver.CarModel = reader.GetString(5);
                            driver.CarColor = reader.GetString(6);

                            myList.Add(driver);
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


    #region UpdateDriver
    public void UpdateDriver(Driver driver)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionDatabase))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandTextUpdateDrivers, connection))
                {
                    cmd.Parameters.AddWithValue("@id", driver.Id);
                    cmd.Parameters.AddWithValue("@fName", driver.Firstname);
                    cmd.Parameters.AddWithValue("@lName", driver.LastName);
                    cmd.Parameters.AddWithValue("@pNumber", driver.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", driver.Email);
                    cmd.Parameters.AddWithValue("@carModel", driver.CarModel);
                    cmd.Parameters.AddWithValue("@carColor", driver.CarColor);

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


    #region DeleteDriver
    public void DeleteDriver(int id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionDatabase))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandTextDeleteDrivers, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine($"ERROR: {ex.Message}");
        }
    }
    #endregion


    #region GetRatingsForDrivers
    public void GetRatingsForDriversAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionDatabase))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandTextGetRaitingForDriver, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fullName = reader.GetString(0);
                            double averageRaiting = reader.GetDouble(1);

                            System.Console.WriteLine($"Driver: {fullName}, Average: {averageRaiting}");
                        }
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


    #region GetOneRaitingForDriver
    public void GetOneRaitingForDriver(int id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionDatabase))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandTextGetRaitingForDriverOne, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fullName = reader.GetString(0);
                            double averageRaiting = reader.GetDouble(1);

                            System.Console.WriteLine($"Driver: {fullName}, Average: {averageRaiting}");
                        }
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


    #region GetDriverRaitingIsNull
    public void GetDriverRaitingIsNull()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionDatabase))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandTextGetRaitingIsNull, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            System.Console.WriteLine($"Driver: {name}");
                        }

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


    #region CommandTextGetDriversHighRating
    public void CommandTextGetDriversHighRating()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionDatabase))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommand.CommandTextGetDriverHighRating, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fullname = reader.GetString(0);
                            double averageRating = reader.GetDouble(1);

                            System.Console.WriteLine($"Full Name: {fullname} / Average: {averageRating}");
                        }
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
}


file class SqlCommand
{
    public const string ConnectionCreateDatabase = "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=12345";
    public const string ConnectionDatabase = "Server=localhost; Port=5432; Database=exam_taxi; User Id=postgres; Password=12345";
    public const string CommandTextCreateDatabase = "CREATE DATABASE exam_taxi";
    public const string CommandTextCreateTable = @"CREATE TABLE drivers
                                                (
                                                    id serial primary key,
                                                    first_name varchar(100) not null,
                                                    last_name varchar(100) not null,
                                                    phone_number varchar(20) not null,
                                                    email varchar(100) not null,
                                                    car_model varchar(100) not null,
                                                    car_color varchar(100) not null
                                                );";
    public const string CommandTextAddDriver = @"INSERT INTO drivers(id, first_name, last_name, phone_number, email, car_model, car_color) VALUES
                                                (@id, @fName, @lName, @pNumber, @email, @carModel, @carColor)";
    public const string CommandTextGetDriver = "SELECT * FROM drivers WHERE id = @id";
    public const string CommandTextGetDrivers = "SELECT * FROM drivers";
    public const string CommandTextUpdateDrivers = "UPDATE drivers SET first_name=@fName, last_name=@lName, phone_number=@pNumber, email=@email, car_model=@carModel, car_color=@carColor WHERE id = @id";

    public const string CommandTextDeleteDrivers = "DELETE FROM drivers WHERE id = @id";

    public const string CommandTextGetRaitingForDriver = @"SELECT CONCAT(d.first_name, ' ', d.last_name) as fullName, 
                                                            AVG(r.ratings) as averageRaiting
                                                            from drivers d
                                                            join trips t on t.driver_id = d.id
                                                            join ratings r on r.trip_id = t.id
                                                            group by CONCAT(d.first_name, ' ', d.last_name)";

    public const string CommandTextGetRaitingForDriverOne = @"SELECT CONCAT(d.first_name, ' ', d.last_name) as fullName, 
                                                            AVG(r.ratings) as averageRaiting
                                                            from drivers d
                                                            join trips t on t.driver_id = d.id
                                                            join ratings r on r.trip_id = t.id
                                                            WHERE d.id = @id
                                                            group by CONCAT(d.first_name, ' ', d.last_name)";

    public const string CommandTextGetRaitingIsNull = @"SELECT CONCAT(d.first_name, ' ', d.last_name) as fullName
                                                        from drivers d
                                                        join trips t ON t.driver_id = d.id
                                                        join ratings r ON r.trip_id = t.id
                                                        WHERE r.ratings IS NULL
                                                        group by CONCAT(d.first_name, ' ', d.last_name)";

    public const string CommandTextGetDriverHighRating = @"SELECT CONCAT(d.first_name, ' ', d.last_name) as fullName,
                                                         AVG(r.ratings) as averageRating
                                                         from drivers d
                                                         join trips t on t.driver_id = d.id
                                                         join ratings r on r.trip_id = t.id
                                                         group by CONCAT(d.first_name, ' ', d.last_name)
                                                         order by averageRating desc
                                                         limit 1";
}