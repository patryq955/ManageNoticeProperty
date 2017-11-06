using ManageNoticeProperty.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public OrderRepository()
        {
            db = new ApplicationDbContext();
        }
        public void Add(Order entity)
        {
            db.Order.Add(entity);
        }

        public void Delete(Order entity)
        {
            db.Order.Remove(entity);
        }

        public Order GetID(int Id)
        {
            return db.Order.Where(x => x.OrderId == Id).FirstOrDefault();
        }

        public IEnumerable<Order> GetOverview(Func<Order, bool> predicate = null)
        {
            if (predicate == null)
            {
                return db.Order;
            }

            return db.Order.Where(predicate);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Order entity)
        {
            db.Entry(entity).State = EntityState.Modified;       
        }
    }
}