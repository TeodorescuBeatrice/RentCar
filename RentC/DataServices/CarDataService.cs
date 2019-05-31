using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RentC.Transports;

namespace RentC.DataServices
{
    public class CarDataService
    {
        public CarDataService(){}

        public Car[] GetCars()
        {
            var client = new WebClient();
            client.Headers.Add("Accept", "application/json");
            var result = client.DownloadString("http://localhost:19923/CarWebService.svc/Cars");
            var serializer = new DataContractJsonSerializer(typeof(Car[]));
            Car[] resultObject;
            using(var stream = new MemoryStream(Encoding.ASCII.GetBytes(result)))
            {
                resultObject = (Car[])serializer.ReadObject(stream);
            }
            return resultObject;
        }

        public Car GetCarById(int carId)
        {          
            var client = new WebClient();
            client.Headers.Add("Accept", "application/json");
            var result = client.DownloadString("http://localhost:19923/CarWebService.svc/Car/" + carId);
            var serializer = new DataContractJsonSerializer(typeof(Car));
            Car resultObject;
            using(var stream = new MemoryStream(Encoding.ASCII.GetBytes(result)))
            {
                resultObject = (Car)serializer.ReadObject(stream);
            }
            return resultObject;
        }

        public Car CreateCar(Car car)
        {
            return SendDataToServer("http://localhost:19923/CarWebService.svc/Cars", "POST", car);
        }

        public Car UpdateCar(Car car)
        {
            return SendDataToServer("http://localhost:19923/CarWebService.svc/Car/" + car.CarID, "PUT", car);
        }

        public void DeleteCar(int carId)
        {
            SendDataToServer("http://localhost:19923/CarWebService.svc/Car/" + carId, "DELETE", 
                new DeleteCar { CarID = carId });
        }

        private T SendDataToServer<T>(string endpoint, string method, T car)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(endpoint);
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Method = method;
            var serializer = new DataContractJsonSerializer(typeof(T));
            var requestStream = request.GetRequestStream();
            serializer.WriteObject(requestStream, car);
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
