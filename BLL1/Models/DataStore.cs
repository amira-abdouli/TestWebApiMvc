using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace BLL1.Models
{
    public static class DataStore<T> where T : BaseData
    {
        public static IEnumerable<T> Get(Expression<Func<T,bool>> query=null)
        {
            var time1 = DateTime.Now;
            if (query == null)
            {
                var result = new DBCon<T>().Table.Where(c => c.Deleted == false).ToList();
                var time2 = DateTime.Now;
                var totaltime = time2 - time1;
                return result;
            }
            else
            {
                //var stringquery = query.Where(c => c.Deleted == false).ToList().ToString();
                //var result = new DBCon<T>().Database.SqlQuery<T>(stringquery);
                var result = new DBCon<T>().Table.Where(query).ToList().Where(c => c.Deleted == false);
                var time2 = DateTime.Now;
                var totaltime = time2 - time1;
                return result;
            }
        }
        public static T Find(params object[] id)
        {
            var result = new DBCon<T>().Table.Find(id);
            if (result.Deleted == false)
                return result;
            else
                return null;
        }
        public static int Add(T model )
        {
            model.ID = Guid.NewGuid();
            model.CreatedDate = DateTime.Now;
            model.UpdatedDate = DateTime.Now;
            //model.ID = new Guid();
            model.Deleted = false;
            var db = new DBCon<T>();
            db.Table.Add(model);
            return db.SaveChanges();
        }
        public static int AddRange(IEnumerable<T> models)
        {
            foreach (T model in models)
            {
                model.ID = Guid.NewGuid();
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                //model.ID = new Guid();
                model.Deleted = false;
            }
            var db = new DBCon<T>();
            db.Table.AddRange(models);
            return db.SaveChanges();
        }
        public static int Update(T model)
        {
            model.UpdatedDate = DateTime.Now;
            var db = new DBCon<T>();
            db.Entry(model).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public static int UpdateRange(IEnumerable<T> models)
        {
            var db = new DBCon<T>();
            foreach(T model in models)
            {
                model.UpdatedDate = DateTime.Now;
                db.Entry(models).State = EntityState.Modified;
            }
            return db.SaveChanges();
        }
        public static int Delete(params object[] id)
        {
            var db = new DBCon<T>();
            var Table = db.Table.Find(id);
            Table.UpdatedDate = DateTime.Now;
            Table.Deleted = true;
            return db.SaveChanges();
        }
        public static int DeleteRange(params object[] ListId)
        {
            var db = new DBCon<T>();
            for (int i = 0; i < ListId.Count(); i++)
            {
                var Table = db.Table.Find(ListId[i]);
                Table.UpdatedDate = DateTime.Now;
                Table.Deleted = true;
            }
            return db.SaveChanges();
        }
        public static object Anonymous()
        {
            var db = new DBCon<T>();
            var db1 = new DBCon<T>();
            var result = from tt in db.Table join ff in db1.Table on tt.ID equals ff.ID select new { tt.ID, tt.Deleted, ff.CreatedDate };
            return result;
        }
    }
}
