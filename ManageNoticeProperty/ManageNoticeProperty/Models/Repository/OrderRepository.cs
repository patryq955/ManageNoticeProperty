using ManageNoticeProperty.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models.Repository
{
    public class OrderRepository : IExtendRepository<Order>
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

        public Order GetIdAll(int id)
        {
            return db.Order.Include("Flat").Include("Flat.User").Include("BuyUser").Where(x => x.OrderId == id).FirstOrDefault();
        }

        public IEnumerable<Order> GetOverview(Func<Order, bool> predicate = null)
        {
            if (predicate == null)
            {
                return db.Order;
            }

            return db.Order.Where(predicate);
        }

        public IEnumerable<Order> GetOverviewAll(Func<Order, bool> predicate = null)
        {
            if (predicate== null)
            {
                return db.Order.Include("Flat").Include("Flat.User").Include("BuyUser");
            }
            return db.Order.Include("Flat").Include("Flat.User").Include("BuyUser").Where(predicate);
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