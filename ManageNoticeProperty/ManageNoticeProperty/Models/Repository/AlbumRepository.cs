using ManageNoticeProperty.DAL;
using ManageNoticeProperty.Models.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models.Repository
{
    public class AlbumRepository : IRepository<Album>
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Add(Album entity)
        {
            db.Album.Add(entity);
        }

        public void Delete(Album entity)
        {
            db.Album.Remove(entity);
        }

        public Album GetID(int Id)
        {
            return db.Album.Where(x => x.AlbumId == Id).FirstOrDefault();
        }

        public IEnumerable<Album> GetOverview(Func<Album, bool> predicate = null)
        {
            if (predicate == null)
            {
                return db.Album;
            }
            return db.Album.Where(predicate);

        }

        public void Update(Album entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}