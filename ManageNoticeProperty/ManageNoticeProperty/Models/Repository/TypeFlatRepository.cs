using ManageNoticeProperty.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models.Repository
{
    public class TypeFlatRepository : IRepository<TypeFlat>
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Add(TypeFlat entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TypeFlat entity)
        {
            throw new NotImplementedException();
        }

        public TypeFlat GetID(int Id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}