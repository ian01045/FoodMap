
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private FoodDeliveryEntities db = null;
        private DbSet<T> DataBaseSet = null;
        public Repository()
        {
            db = new FoodDeliveryEntities();
            DataBaseSet = db.Set<T>();
            //db.Set將Northwind實體模型資料傳回給定實體類型的集合。
            //Set<TEntity>() || 傳回 DbSet<TEntity> 執行個體來存取內容中給定類型的實體和基礎存放區。
        }

        public void Delete(T entity)
        {
            DataBaseSet.Remove(entity);
            db.SaveChanges();
        }

        public IEnumerable<T> GetAllInfo()
        {
            return DataBaseSet.ToList();
        }

        public T GetID(int ID)
        {
            return DataBaseSet.Find(ID);
            //Find會根據表單裡面的主索引鍵去尋找
        }

        public void Insert(T entity)
        {
            DataBaseSet.Add(entity);
            db.SaveChanges();
        }

        public void Update(T entity)
        {

            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal T GetById(int id)
        {
            return DataBaseSet.Find(id);
        }
    }
}