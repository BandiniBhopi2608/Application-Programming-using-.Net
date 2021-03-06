﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccessLayer
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> dbSet;

        public Repository(DbContext datacontext)
        {
            //You can use the cpmt
            dbSet = datacontext.Set<T>();
            this.context = datacontext;
        }

        public void Insert(T entity)
        {
            //Use the context object and entity state to save the entity
            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            //Use the context object and entity state to delete the entity
            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Update(T entity)
        {

            //Use the context object and entity state to update the entity            
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        //This method will find the related records by passing two argument
        //First argument: lambda expression to search a record such as d => d.StandardName.Equals(standardName) to search am record by standard name
        //Second argument: navigation property that leads to the related records such as d => d.Students
        //The method returns the related records that met the condition in the first argument.
        //An example of the method GetStandardByName(string standardName)
        //public Standard GetStandardByName(string standardName)
        //{
        //return _standardRepository.GetSingle(d => d.StandardName.Equals(standardName), d => d.Students);
        //}
        {
            T item = null;
            IQueryable<T> dbQuery = dbSet;
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                   .AsNoTracking()
                   .FirstOrDefault(where);
            
            return item;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
