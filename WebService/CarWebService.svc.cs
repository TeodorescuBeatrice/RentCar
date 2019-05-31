using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using RentC;
using RentC.DataServices;
using RentC.Entities;

namespace WebService
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CarWebService
    {
        private CarService _service;
        public CarWebService()
        {
            _service = new CarService();
        }
        [WebGet(UriTemplate = "/Cars")]
        public Car[] GetCars()
        {
            return _service.GetCars();
        }

        [WebGet(UriTemplate = "/Car/{carId}")]
        public Car GetCarById(string carId)
        {
            Int32.TryParse(carId, out int parsedCarId);
            return _service.GetCarByID(parsedCarId);
        }

        [WebInvoke(UriTemplate = "/Cars")]
        public void CreateCar(Car car)
        {
            _service.AddCar(car);
        }

        [WebInvoke(Method = "PUT", UriTemplate = "/Car/{carId}")]
        public void UpdateCar(string carId, Car car)
        {
            _service.UpdateCar(car);
        }

        [WebInvoke(Method = "DELETE", UriTemplate = "/Car/{carId}")]
        public void DeleteCar(string carId)
        {
            Int32.TryParse(carId, out int parsedCarId);
            _service.DeleteCar(parsedCarId);
        }
    }
}
