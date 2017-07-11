using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryTemplete.Models
{
    public interface IRepository<T>
    {
        //T為一泛型，其可以存放任一型別的值
        T GetID(int ID);
        IEnumerable<T> GetAllInfo();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
