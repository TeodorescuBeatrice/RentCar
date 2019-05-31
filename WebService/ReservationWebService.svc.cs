 using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Data.DataServices;
using RentC;
using RentC.Entities;

namespace WebService
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReservationWebService
    {
        private ReservationService _service;
        public ReservationWebService()
        {
            _service = new ReservationService();
        }

        [WebGet(UriTemplate = "/Reservations")]
        public Reservation[] GetReservations()
        {
            return _service.GetReservations();
        }

        [WebGet(UriTemplate = "/Reservation/{CarId}/{CustomerId}")]
        public Reservation GetReservationById(string CarId, string CustomerId)
        {
            int.TryParse(CarId, out int parsedCarId);
            int.TryParse(CustomerId, out int parsedCustomerId);
            return _service.GetReservationByID(parsedCarId, parsedCustomerId);
        }

        [WebInvoke(UriTemplate = "/Reservations")]
        public void CreateReservation(Reservation Reservation)
        {
            _service.AddReservation(Reservation);
        }

        [WebInvoke(Method = "PUT", UriTemplate = "/Reservation/{CarId}/{CustomerId}")]
        public void UpdateReservation(string CarId, string CustomerId, Reservation Reservation)
        {
            _service.UpdateReservation(Reservation);
        }

        [WebInvoke(Method = "DELETE", UriTemplate = "/Reservation/{CarId}/{CustomerId}")]
        public void DeleteReservation(string CarId, string CustomerId)
        {
            int.TryParse(CarId, out int parsedCarId);
            int.TryParse(CustomerId, out int parsedCustomerId);
            _service.DeleteReservation(parsedCarId, parsedCustomerId);
        }
    }
}
