using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentC.Transports
{
    public class DeleteReservation
    {
        [Key]
        public int CarID { get; set; }

        [Key]
        public int CustomerID { get; set; }
    }
}
