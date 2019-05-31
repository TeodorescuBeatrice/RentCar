using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using RentC.Transports;

namespace RentC.DataServices
{
    public class ReservationDataService
    {
        public ReservationDataService() { }

        public Reservation[] GetReservations()
        {
            var client = new WebClient();
            client.Headers.Add("Accept", "application/json, text/html");
            var result = client.DownloadString("http://localhost:19923/ReservationWebService.svc/Reservations");
            var serializer = new DataContractJsonSerializer(typeof(Reservation[]));
            Reservation[] resultObject;
            using(var stream = new MemoryStream(Encoding.ASCII.GetBytes(result)))
            {
                resultObject = (Reservation[])serializer.ReadObject(stream);
            }
            return resultObject;
        }

        public Reservation GetReservationById(int CarId, int CustomerId)
        {
            var client = new WebClient();
            client.Headers.Add("Accept", "application/json");
            var result = client.DownloadString("http://localhost:19923/ReservationWebService.svc/Reservation/" + CarId + "/" +CustomerId);
            var serializer = new DataContractJsonSerializer(typeof(Reservation));
            Reservation resultObject;
            using(var stream = new MemoryStream(Encoding.ASCII.GetBytes(result)))
            {
                resultObject = (Reservation)serializer.ReadObject(stream);
            }
            return resultObject;
        }

        public Reservation CreateReservation(Reservation Reservation)
        {
            return SendDataToServer("http://localhost:19923/ReservationWebService.svc/Reservations", "POST", Reservation);
        }

        public Reservation UpdateReservation(Reservation Reservation)
        {
            return SendDataToServer("http://localhost:19923/ReservationWebService.svc/Reservation/" + Reservation.CarID + "/" + Reservation.CostumerID, "PUT", Reservation);
        }

        public void DeleteReservation(int CarId, int CustomerId)
        {
            SendDataToServer("http://localhost:19923/ReservationWebService.svc/Reservation/" + CarId +"/" + CustomerId, "DELETE",
                new DeleteReservation {
                    CarID = CarId,
                    CustomerID = CustomerId
                });
        }

        private T SendDataToServer<T>(string endpoint, string method, T Reservation)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(endpoint);
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Method = method;
            var serializer = new DataContractJsonSerializer(typeof(T));
            var requestStream = request.GetRequestStream();
            serializer.WriteObject(requestStream, Reservation);
            requestStream.Close();
            var response = request.GetResponse();
            if(response.ContentLength == 0)
            {
                response.Close();
                return default(T);
            }
            var responseStream = response.GetResponseStream();
            T responseObject = (T)serializer.ReadObject(responseStream);
            responseStream.Close();
            return responseObject;
        }
    }
}
