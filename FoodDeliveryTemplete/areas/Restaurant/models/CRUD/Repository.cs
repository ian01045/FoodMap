using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD
{
    public class Repository<T> : RepositoryInterface<T> where T : class
    {
        FoodDeliveryEntities db = null;
        DbSet<T> Dbset = null;
        public Repository()
        {
            db = new FoodDeliveryEntities();
            Dbset = db.Set<T>();
        }
        public void Create(T _all)
        {
            Dbset.Add(_all);
            db.SaveChanges();
        }

        public void Delete(T _all)
        {
            try
            {
                Dbset.Remove(_all);
                db.SaveChanges();
            }
            catch { }
            
            
        }

        public IEnumerable<T> GetAll()
        {
            return Dbset.ToList();
        }

        public T getbyid(int? id)
        {
            return Dbset.Find(id);
        }

        public void Update(T _all)
        {
            db.Entry(_all).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}