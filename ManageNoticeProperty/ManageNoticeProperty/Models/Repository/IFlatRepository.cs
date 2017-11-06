using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageNoticeProperty.Models.Repository
{
    public interface IFlatRepository : IRepository<Flat>
    {
        IEnumerable<Flat> GetOverviewAll(Func<Flat, bool> predicate);
    }
}
