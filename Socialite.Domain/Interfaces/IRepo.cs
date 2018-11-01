using Socialite.Domain.Common;
using System.Collections.Generic;

namespace Socialite.Domain.Interfaces
{
    public interface IRepo
    {
        T FindById<T>(int id) where T : BaseEntity;

        List<T> FindAll<T>() where T : BaseEntity;

        T Add<T>(T entity) where T : BaseEntity;

        void Update<T>(T entity) where T : BaseEntity;

        void Delete<T>(T entity) where T : BaseEntity;
    }
}