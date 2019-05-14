using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentC.Entities;

namespace RentC
{
    public class RentCDb : DbContext
    {
        public RentCDb(string connectionString)
        {
          
            Database.SetInitializer<RentCDb>(new CreateDatabaseIfNotExists<RentCDb>());
           
            this.Database.Connection.ConnectionString = connectionString;
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolesPermission> RolesPermissions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
