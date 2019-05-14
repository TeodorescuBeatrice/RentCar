using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentC.Entities;

namespace RentC
{
    public class Car
    {
        [Key]
        public int CarID { get; set; }
        public string Plate { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
