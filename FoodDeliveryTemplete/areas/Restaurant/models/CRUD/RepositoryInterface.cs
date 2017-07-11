using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD
{
    interface RepositoryInterface<T>
    {
        T getbyid(int? id);
        IEnumerable<T> GetAll();
        void Create(T _all);
        void Update(T _all);
        void Delete(T _all);
    }
}
