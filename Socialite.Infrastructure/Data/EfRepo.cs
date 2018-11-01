using Microsoft.EntityFrameworkCore;
using Socialite.Domain.Common;
using Socialite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Socialite.Infrastructure.Data
{
    public class EfRepo : IRepo
    {
        private readonly SocialiteDbContext dbContext;

        public EfRepo(SocialiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public T Add<T>(T entity) where T : BaseEntity
        {
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();

            return entity;
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            dbContext.Set<T>().Remove(entity);
            dbContext.SaveChanges();
        }

        public List<T> FindAll<T>() where T : BaseEntity
        {
            return dbContext.Set<T>().ToList();
        }

        public T FindById<T>(int id) where T : BaseEntity
        {
            return dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
