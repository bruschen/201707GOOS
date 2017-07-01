using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOOS_Sample.Repository
{
    public interface IRepository<T>
    {
        void Save(T entity);
        T Read(Func<T, bool> predicate);
    }
}
