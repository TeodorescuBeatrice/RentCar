using System.Linq;
using RentC;
using RentC.Entities;

namespace Data.DataServices
{
    public class ReservationService
    {
        private RentCDb _dbContext;
        public ReservationService()
        {
            _dbContext = new RentCDb();
        }
        public Reservation[] GetReservations()
        {
            return _dbContext.Reservations.Select(r=>r).ToArray();     
        }

        public Reservation GetReservationByID(int CarId, int CustomerId)
        {
            return _dbContext.Reservations.SingleOrDefault(r => ((r.CarID == CarId) && (r.CostumerID == CustomerId)));
        }

        public void AddReservation(Reservation res)
        {
            _dbContext.Reservations.Add(res);
            _dbContext.SaveChanges();
        }

        public void UpdateReservation(Reservation res)
        {
            _dbContext.Reservations.Add(res);
            _dbContext.Entry(res).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void DeleteReservation(int CarId, int CustomerId)
        {
            var res = _dbContext.Reservations.SingleOrDefault(r => ((r.CarID == CarId) && (r.CostumerID == CustomerId)));
            _dbContext.Reservations.Remove(res);
            _dbContext.SaveChanges();
        }
    }
}
