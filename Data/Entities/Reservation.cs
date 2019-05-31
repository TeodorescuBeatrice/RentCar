using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RentC.Entities;

namespace RentC.Entities
{
    [Serializable]
    [DataContract]
    public class Reservation
    {    
        [Key, Column(Order = 0)]
        [DataMember]
        public int CarID { get; set; }
        public Car Car { get; set; } //fk

        [Key, Column(Order = 1)]
        [DataMember]
        public int CostumerID { get; set; }
        public Customer Customer { get; set; } //fk

        [DataMember]
        public Byte ReservStatsID { get; set; } //fk
        public ReservationStatus ReservStats { get; set; }

        [DataMember]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataMember]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string CouponCode { get; set; } //fk
        public Coupon Coupon { get; set; }
    }
}
