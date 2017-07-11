using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.Advertisement.Models.Partials
{
    public class AdvRepository<T> : IAdvRepository<T> where T : class
    {
        private FoodDeliveryEntities db = null;
        private DbSet<T> Dbset = null;

        public AdvRepository()
        {
            db = new FoodDeliveryEntities();
            Dbset = db.Set<T>();
        }

        public void Create(T _entity)
        {
            Dbset.Add(_entity);
            db.SaveChanges();
        }

        public void Delete(T _entity)
        {
            Dbset.Remove(_entity);
            db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return Dbset.ToList();
        }

        public T GetById(int id)
        {
            return Dbset.Find(id);
        }

        public void Update(T _entity)
        {
            db.Entry(_entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}