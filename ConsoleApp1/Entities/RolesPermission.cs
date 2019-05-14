using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentC.Entities
{
    public class RolesPermission
    {
        [Key, Column(Order = 0)]
        public int RoleID { get; set; }
        public Role Role { get; set; } //fk

        [Key, Column(Order = 1)]
        public int PermissionID { get; set; }
        public Permission Permission { get; set; } //fk
    }
}
