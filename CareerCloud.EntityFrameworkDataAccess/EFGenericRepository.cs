using CareerCloud.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Collections;


namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T : class

    {
        private CareerCloudContext MyCareerCloudContext= new CareerCloudContext();

        public void Add(params T[] items)
        {
            foreach (var itemEntity in items)
            {
                MyCareerCloudContext.Entry(itemEntity).State = EntityState.Added;
                MyCareerCloudContext.SaveChanges();
            }

            #region Also_Works
            //// ---Also works
            //if (items != null)
            //{
            //    var dataSet = MyCareerCloudContext.Set<T>();

            //    if (items is IEnumerable)
            //    {
            //        dataSet.AddRange(items);
            //    }
            //    else
            //    {
            //        dataSet.Add(items.Where(p => p != null).FirstOrDefault());
            //    }

            //    MyCareerCloudContext.SaveChanges();
            //}


            //// ---Also works
            //MyCareerCloudContext.Add(items.Where(p => p != null).FirstOrDefault());  //.Set<T>().Add(items.FirstOrDefault());
            //MyCareerCloudContext.SaveChanges();

            #endregion



        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {

            IQueryable<T> dbQuery = MyCareerCloudContext.Set<T>();
            foreach (var navProp in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(navProp);
            }
            return dbQuery.ToList();

          //// ---Also works
          //  return MyCareerCloudContext.Set<T>().ToList<T>();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = MyCareerCloudContext.Set<T>();
            foreach (var navProp in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(navProp);
            }
            return dbQuery.Where(where).ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {

            {
                IQueryable<T> dbQuery = MyCareerCloudContext.Set<T>();
                foreach (Expression<Func<T, object>> navProp in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navProp);
                }
                return dbQuery.FirstOrDefault(where);
            }

            //// ---Also works
            // IQueryable<T> poco = GetAll().AsQueryable();
            // poco = poco.Where(p => p != null);
            //return poco.Where(where).FirstOrDefault<T>();

        }

        public void Remove(params T[] items)
        {
            foreach (var itemEntity in items)
            {
                MyCareerCloudContext.Entry(itemEntity).State = EntityState.Deleted;
                MyCareerCloudContext.SaveChanges();
            }

            ////Also works
            //MyCareerCloudContext.RemoveRange(items); //.Remove(items);
            //MyCareerCloudContext.SaveChanges();
        }

        public void Update(params T[] items)
        {

            foreach (var itemEntity in items)
            {
                MyCareerCloudContext.Entry(itemEntity).State = EntityState.Modified;
                MyCareerCloudContext.SaveChanges();
            }
            ////Also works
            //MyCareerCloudContext.UpdateRange(items);
            //MyCareerCloudContext.SaveChanges();
        }
    }

}
