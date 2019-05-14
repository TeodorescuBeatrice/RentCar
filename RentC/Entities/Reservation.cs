using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentC.Entities
{
    public class Reservation
    {
        [Key, Column(Order = 0)]
        public int CarID { get; set; }
        public Car Car { get; set; } //fk

        [Key, Column(Order = 1)]
        public int CostumerID { get; set; }
        public Customer Customer { get; set; } //fk

        public int ReservStatsID { get; set; } //fk
        public ReservationStatus ReservStats { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }

        public string CouponCode { get; set; } //fk
        public Coupon Coupon { get; set; }
    }
}
