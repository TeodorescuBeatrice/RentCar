using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RentC.Transports
{
    public class Reservation
    {
        public int CarID { get; set; }

        public int CostumerID { get; set; }

        public Byte ReservStatsID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public string Location { get; set; }

        public string CouponCode { get; set; }
    }
}
