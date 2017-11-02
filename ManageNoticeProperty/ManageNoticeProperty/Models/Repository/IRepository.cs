using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageNoticeProperty.Models.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetID(int Id);
        IEnumerable<T> GetOverview(Func<T, bool> predicate = null);
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

    }
}
