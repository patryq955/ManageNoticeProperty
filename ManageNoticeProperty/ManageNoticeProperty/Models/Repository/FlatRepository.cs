using ManageNoticeProperty.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models.Repository
{
    public class FlatRepository : IRepository<Flat>
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
            return db.Flat.Where(x => x.FlatId == Id).FirstOrDefault();
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
    }
}