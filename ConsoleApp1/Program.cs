using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace RentC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = "DESKTOP-A0CPMR3\\SQLEXPRESS",
                    InitialCatalog = "academy_net",
                    IntegratedSecurity = true
                };

                using(RentCDb context = new RentCDb(builder.ConnectionString))
                {
                    Console.WriteLine("Reading data from table Cars:");
                    var query1 = from c in context.Cars
                                select c;
                    foreach(var c in query1.Take(10))
                    {
                        Console.WriteLine($"{c.Manufacturer}, {c.Model}, {c.Plate}");
                    }

                    Console.WriteLine("Reading data from table Coupons:");
                    var query2 = from c in context.Coupons
                                select c;
                    foreach(var c in query2.Take(10))
                    {
                        Console.WriteLine($"{c.CouponCode}, {c.Discount}");
                    }

                    Console.WriteLine("Reading data from table Customers:");
                    var query3 = from c in context.Customers
                                 select c;
                    foreach(var c in query3.Take(10))
                    {
                        Console.WriteLine($"{c.Name}, {c.BirthDate}");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
