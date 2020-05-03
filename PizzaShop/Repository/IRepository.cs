using System.Collections.Generic;

namespace PizzaShop.Repository
{
    public interface IRepository<T,Tk>
    {
        public IEnumerable<T> GetAll();
        public T GetById(Tk id);
        public T Update(T item);
        public void Create(T item);
        public void Delete(Tk id);

    }
}