using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentC.Entities;

namespace RentC.DataServices
{
    public class CarService
    {
        private RentCDb _dbContext;
        public CarService()
        {
            _dbContext = new RentCDb();
        }
        public Car[] GetCars()
        {
            return _dbContext.Cars.Select(c=>c).Take(10).ToArray();
        }

        public Car GetCarByID(int CarId)
        {
            return _dbContext.Cars.SingleOrDefault(c => c.CarID == CarId);
        }

        public void AddCar(Car car)
        {
            int lastId = _dbContext.Cars.Max(c => c.CarID);
            car.CarID = lastId++;
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();
        }

        public void UpdateCar(Car car)
        {
            _dbContext.Cars.Add(car);
            _dbContext.Entry(car).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void DeleteCar(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.CarID == id);
            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();
        }
    }
}
