using ManageNoticeProperty.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models.Repository
{
    public class TypeFlatRepository : IRepository<TypeFlat>
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Add(TypeFlat entity)
        {
            db.TypeFlat.Add(entity);
        }

        public void Delete(TypeFlat entity)
        {
            db.TypeFlat.Remove(entity);
        }

        public TypeFlat GetID(int Id)
        {
            return db.TypeFlat.Where(x => x.TypeFlatID == Id).FirstOrDefault();
        }

        public IEnumerable<TypeFlat> GetOverview(Func<TypeFlat, bool> predicate = null)
        {
            if (predicate == null)
            {
                return db.TypeFlat;
            }
            return db.TypeFlat.Where(predicate);
        }

        public void Update(TypeFlat entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
        
        public void Save()
        {
            db.SaveChanges();
        }
    }
}