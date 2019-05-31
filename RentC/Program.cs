using System;
using RentC.DataServices;
using RentC.Transports;

namespace RentC
{
    internal class Program
    {
        private static CarDataService _service;
        private static ReservationDataService _resService;

        private static void Main(string[] args)
        {
            _service = new CarDataService();
            _resService = new ReservationDataService();
            string input = "";
            while(input.ToUpper() != "Q")
            {
                Console.WriteLine("L) List Cars");
                Console.WriteLine("#) Show specific Car");
                Console.WriteLine("N) Enter a new Car");
                Console.WriteLine("Q) Quit");
                Console.Write("Please enter a command: ");
                input = Console.ReadLine().ToUpper();
                int index = 0;
                if(input == "L")
                {
                    ListCars();
                }
                else if(input == "N")
                {
                    EnterCar();
                }
                else if(int.TryParse(input, out index))
                {
                    ShowCar(index);
                }

            }
            input = "";
            while(input.ToUpper() != "Q")
            {
                Console.WriteLine("L) List Reservations");
                Console.WriteLine("#) Show specific Reservation");
                Console.WriteLine("N) Enter a new Reservation");
                Console.WriteLine("Q) Quit");
                Console.Write("Please enter a command: ");
                input = Console.ReadLine().ToUpper();
                
                if(input == "L")
                {
                    ListReservations();
                }
                else if(input == "N")
                {
                    EnterReservation();
                }
                else
                {
                    int carId = int.Parse(Console.ReadLine());
                    int costumerId = int.Parse(Console.ReadLine());
                    ShowReservation(carId, costumerId);
                }

            }
        }
        private static void ShowCar(int index)
        {
            var car = _service.GetCarById(index);
            Console.WriteLine("{0,-2} | {1,-10} | {2,-13} | {3,-13} | {4,-10}", "ID", "Plate", "Manufacturer", "Model", "PricePerDay");
            Console.WriteLine(String.Format("{0,-2} | {1,-10} | {2,-13} | {3,-13} | {4,-10}", car.CarID, car.Plate, car.Manufacturer, car.Model, car.PricePerDay));
            Console.WriteLine("---------------");
            Console.WriteLine("E) Edit car");
            Console.WriteLine("D) Delete car");
            Console.WriteLine("C) Continue");
            Console.Write("Please enter a command: ");
            var input = Console.ReadLine().ToUpper();
            if(input == "E")
            {
                EditCar(index);
            }
            else if(input == "D")
            {
                DeleteCar(index);
            }
        }
        private static void DeleteCar(int index)
        {
            Console.WriteLine("---------------");
            Console.Write("Are you sure you want to delete this car? (Y/N) ");
            var input = Console.ReadLine().ToUpper();
            if(input == "Y")
            {
                _service.DeleteCar(index);
            }
        }

        private static void EditCar(int index)
        {
            Console.WriteLine("---------------");
            Console.Write("Plate: ");
            var plate = Console.ReadLine();
            Console.Write("Manufacturer: ");
            var man = Console.ReadLine();
            Console.Write("Model: ");
            var model = Console.ReadLine();
            Console.Write("PricePerDay: ");
            var ppd = decimal.Parse(Console.ReadLine());
            var car = new Car
            {
                CarID = index,
                Plate = plate,
                Manufacturer = man,
                Model = model,
                PricePerDay = ppd
            };
            _service.UpdateCar(car);
        }

        private static void EnterCar()
        {
            Console.WriteLine("---------------");
            Console.Write("Plate: ");
            var plate = Console.ReadLine();
            Console.Write("Manufacturer: ");
            var man = Console.ReadLine();
            Console.Write("Model: ");
            var model = Console.ReadLine();
            Console.Write("PricePerDay: ");
            var ppd = decimal.Parse(Console.ReadLine());
            var car = new Car
            {
                Plate = plate,
                Manufacturer = man,
                Model = model,
                PricePerDay = ppd
            };
            _service.CreateCar(car);
        }

        private static void ListCars()
        {
            var cars = _service.GetCars();
            Console.WriteLine("{0,-2} | {1,-10} | {2,-13} | {3,-13} | {4,-10}","ID" , "Plate" , "Manufacturer", "Model" , "PricePerDay");
            Console.WriteLine("---------------------------------------------------------------------------------");
            foreach(var car in cars)
            {
                Console.WriteLine(String.Format("{0,-2} | {1,-10} | {2,-13} | {3,-13} | {4,-10}",car.CarID,car.Plate,car.Manufacturer,car.Model,car.PricePerDay));
            }
            Console.WriteLine("---------------------------------------------------------------------------------");
        }

        private static void ShowReservation(int carId, int customerId)
        {
            var res = _resService.GetReservationById(carId, customerId);
            Console.WriteLine("{0,-20} | {1,-10} | {2,-13} | {3,-13} | {4,-10}", "Reservation Status", "Start Date", "End Date", "Location", "Coupon Code");
            Console.WriteLine(String.Format("{0,-20} | {1,-10} | {2,-13} | {3,-13} | {4,-10}", res.ReservStatsID, res.StartDate.ToShortDateString(), res.EndDate.ToShortDateString(), res.Location, res.CouponCode));

            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine("E) Edit Reservation");
            Console.WriteLine("D) Delete Reservation");
            Console.WriteLine("C) Continue");
            Console.Write("Please enter a command: ");
            var input = Console.ReadLine().ToUpper();
            if(input == "E")
            {
                EditReservation(carId, customerId);
            }
            else if(input == "D")
            {
                DeleteReservation(carId, customerId);
            }
        }
        private static void DeleteReservation(int carId, int customerId)
        {
            Console.WriteLine("---------------");
            Console.Write("Are you sure you want to delete this reservation? (Y/N) ");
            var input = Console.ReadLine().ToUpper();
            if(input == "Y")
            {
                _resService.DeleteReservation(carId, customerId);
            }
        }

        private static void EditReservation(int carId, int customerId)
        {
            Console.WriteLine("---------------");
            Console.Write("ReservStatsID: ");
            var reservId = Byte.Parse(Console.ReadLine());
            Console.Write("StartDate: ");
            var sd = DateTime.Parse(Console.ReadLine());
            Console.Write("EndDate: ");
            var ed = DateTime.Parse(Console.ReadLine());
            Console.Write("Location: ");
            var loc = Console.ReadLine();
            Console.Write("CouponCode: ");
            var coupon = Console.ReadLine();
            var res = new Reservation
            {
                CarID = carId,
                CostumerID = customerId,
                ReservStatsID = reservId,
                StartDate = sd,
                EndDate = ed,
                Location = loc,
                CouponCode = coupon
            };
            _resService.UpdateReservation(res);
        }

        private static void EnterReservation()
        {
            Console.WriteLine("---------------");
            Console.Write("CarID: ");
            var carId = Int32.Parse(Console.ReadLine());
            Console.Write("CustomerID: ");
            var customerId = Int32.Parse(Console.ReadLine());
            Console.Write("ReservStatsID: ");
            var reservStatsId = Byte.Parse(Console.ReadLine());
            Console.Write("StartDate: ");
            var startDate = DateTime.Parse(Console.ReadLine());
            Console.Write("EndDate: ");
            var endDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Location: ");
            var location = Console.ReadLine();
            Console.Write("CouponCode: ");
            var coupon = Console.ReadLine();
            var res = new Reservation
            {
                CarID = carId,
                CostumerID = customerId,
                ReservStatsID = reservStatsId,
                StartDate = startDate,
                EndDate = endDate,
                Location=location,
                CouponCode=coupon
            };
            _resService.CreateReservation(res);
        }

        private static void ListReservations()
        {
            var res = _resService.GetReservations();
            Console.WriteLine("{0,-6} | {1,-11} | {2,-20} | {3,-10} | {4,-13} | {5,-13} | {6,-10}","CarID", "CustomerID", "Reservation Status", "Start Date", "End Date", "Location", "Coupon Code");
            foreach(var r in res)
            {
                Console.WriteLine(String.Format("{0,-6} | {1,-11} | {2,-20} | {3,-10} | {4,-13} | {5,-13} | {6,-10}",r.CarID,r.CostumerID, r.ReservStatsID, r.StartDate.ToShortDateString(), r.EndDate.ToShortDateString(), r.Location, r.CouponCode));
            }
            Console.WriteLine("---------------");
        }
    }
}