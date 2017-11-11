using ManageNoticeProperty.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models.Repository
{
    public class FlatRepository : IExtendRepository<Flat>
    {
        ApplicationDbContext db;
        public FlatRepository()
        {
            db = new ApplicationDbContext();
        }
        public void Add(Flat entity)
        {
            db.Flat.Add(entity);
        }

        public void Delete(Flat entity)
        {
            db.Flat.Remove(entity);
        }

        public Flat GetID(int Id)
        {
            return db.Flat.Include("Album").Where(x => x.FlatId == Id).FirstOrDefault();
        }

        public IEnumerable<Flat> GetOverview(Func<Flat, bool> predicate = null)
        {
            if (predicate == null)
            {
                return db.Flat;
            }
            return db.Flat.Where(predicate);
        }

        public void Update(Flat entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Flat> GetOverviewAll(Func<Flat, bool> predicate = null)
        {
            if (predicate == null)
            {
                return db.Flat.Include("Order").Include("User").Include("Order.BuyUser").Include("Album");
            }

            return db.Flat.Include("Order").Include("User").Include("Order.BuyUser").Include("Album").Where(predicate);
        }

        public Flat GetIdAll(int id)
        {
            return db.Flat.Include("Order").Include("User").Include("Order.BuyUser").Include("Album").Where(x => x.FlatId == id).FirstOrDefault();
        }
    }
}