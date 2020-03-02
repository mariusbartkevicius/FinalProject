using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_MoviesDB.Db
{
    public interface IRepo<T>
    {
        T Get(int id);
        List<T> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        List<T> Find(string text);
        List<T> OrderBy(string text);
        List<T> GroupBy(string text);
    }
}
