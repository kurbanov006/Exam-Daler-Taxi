// DriverService.CreateDatabase();
// DriverService.CreateTable();


DriverService driverService = new DriverService();


Driver driver = new Driver()
{
    Id = 1,
    Firstname = "Dalerboy",
    LastName = "Dalerovich",
    PhoneNumber = "+992 332 23 1234",
    Email = "daler@gmail.com",
    CarModel = "Mersedes CLS-63",
    CarColor = "Black"
};
// UPDATE DRIVER
// driverService.UpdateDriver(driver);



// DELETE ONE DRIVER
// driverService.DeleteDriver(0);


// ADD DRIVER 
// driverService.AddDriver(driver);


// //  GET ONE DRIVER
// var get = driverService.GetDriver(1);
// System.Console.WriteLine($"Id: {get.Id}, FirstName: {get.Firstname}, LastName: {get.LastName}, PhoneNumber: {get.PhoneNumber}, Email: {get.Email}, CarModel: {get.CarModel}, Carcolor: {get.CarColor}");


// // GET ALL DRIVERS
// var getAll = driverService.GetDrivers();
// foreach (var item in getAll)
// {
//     System.Console.WriteLine($"Id: {item.Id}, FirstName: {item.Firstname}, LastName: {item.LastName}, PhoneNumber: {item.PhoneNumber}, Email: {item.Email}, CarModel: {item.CarModel}, Carcolor: {item.CarColor}");
// }




// PassengerService passengerService = new PassengerService();

// PassengerService.CreateTable();


PassengerService passengerService = new PassengerService();

Passenger passenger = new Passenger()
{
    Id = 3,
    FirstName = "Jeck",
    LastName = "Jeckson",
    PhoneNumber = "+992 165 44 11",
    Email = "jeck@gmail.ru"
};


// GET ONE PASSENGER BY ID
// var getPas = passengerService.GetPassenger(3);
// System.Console.WriteLine($"Id: {getPas.Id}, FirstName: {getPas.FirstName}, LastName: {getPas.LastName}, PhoneNumber: {getPas.PhoneNumber}, Email: {getPas.Email}");


// DELETE PASSENGER
// // passengerService.DeletePassenger(3);


// UPDATE PASSENGER
// // passengerService.UpdatePassenger(passenger);


// ADD ONE PASSENGERS
// passengerService.AddPassenger(passenger);

// GET ALL PASSENGERS
// var getAllPas = passengerService.GetPassengers();
// foreach (var item in getAllPas)
// {
//     System.Console.WriteLine($"Id: {item.Id}, FirstName: {item.FirstName}, LastName: {item.LastName}, PhoneNumber: {item.PhoneNumber}, Email: {item.Email}");
// }





// CREATE TABLE TRIPS
// TripService.CreateTable();

// DROP TABLE
// TripService.DropTable();
TripService tripService = new TripService();

var timeStart = new TimeSpan(12, 23, 54);
var timeEnd = new TimeSpan(13, 01, 12);

Trips trips = new Trips()
{
    Id = 1,
    Driver_id = 1,
    Passenger_id = 1,
    StartLocation = "Bozori Sadbarg",
    EndLocation = "83 MKR",
    StartTime = timeStart,
    EndTime = timeEnd,
    Pay = 35
};
// ADD TRIPS 
// tripService.AddTrip(trips);

// var getTrip = tripService.GetTrip(1);
// System.Console.WriteLine($"Id: {getTrip.Id}, Driver Id: {getTrip.Driver_id}, Passenger Id: {getTrip.Passenger_id}, StartLocation: {getTrip.StartLocation}, EndLocation: {getTrip.EndLocation}, Start Time: {getTrip.StartTime}, End Time: {getTrip.EndTime}, Pay: {getTrip.Pay}");

// UPDATE TRIPS
// tripService.UpdateTrips(trips);


// DELETE TRIPS
// tripService.DeleteTrips(1);


// System.Console.WriteLine();
// GET ALL TRIPS
// var getAllTrips = tripService.GetTrips();
// foreach (var item in getAllTrips)
// {
//     System.Console.WriteLine($"Id: {item.Id}, Driver Id: {item.Driver_id}, Passenger Id: {item.Passenger_id}, StartLocation: {item.StartLocation}, EndLocation: {item.EndLocation}, Start Time: {item.StartTime}, End Time: {item.EndTime}, Pay: {item.Pay}");
// }







// CREATE TABLE RATING
// RatingService.CreateTable();
// RatingService.DeleteTable();

RatingService ratingService = new RatingService();

Rating rating = new Rating()
{
    Id = 1,
    Driver_id = 1,
    Trips_id = 1,
    Passenger_id = 1,
    Ratings = 3,
    Comment = "Good"
};

// ADD RATING
// ratingService.AddRating(rating);


// UPDATE RATING
// ratingService.UpdateRating(rating);


// GET ONE RATING
var getRating = ratingService.GetRating(1);
// System.Console.WriteLine($"ID: {getRating.Id}, Driver Id: {getRating.Driver_id}, Trip Id: {getRating.Trips_id}, Passenger Id: {getRating.Passenger_id}, Rating: {getRating.Ratings}, Comment: {getRating.Comment}");


//DELETE RATING
// ratingService.DeleteRating(1);

System.Console.WriteLine();
// GET ALL RATINGS 
var getRatings = ratingService.GetRatings();
// foreach (var item in getRatings)
// {
//     Console.WriteLine($"ID: {item.Id}, Driver Id: {item.Driver_id}, Trip Id: {item.Trips_id}, Passenger Id: {item.Passenger_id}, Rating: {item.Ratings}, Comment: {item.Comment}");
// }








// -------------  ЗАПРОСЫ  ------------ // 
// Выдаёт всех водителей и их рейтинг
// 1
driverService.GetRatingsForDriversAll();
System.Console.WriteLine();


// -------------  ЗАПРОСЫ  ------------ // 
// Выдает одного водителя рейтинг по ID
// 2
driverService.GetOneRaitingForDriver(1);
System.Console.WriteLine();



// -------------  ЗАПРОСЫ  ------------ // 
// Выдает одного водителя который не получил рейтинг
// 3
driverService.GetDriverRaitingIsNull();
System.Console.WriteLine();



// -------------  ЗАПРОСЫ  ------------ // 
// Выдает водителей по рейтингам  У кого больше средний рейтинг  
// 4
driverService.CommandTextGetDriversHighRating();
System.Console.WriteLine();