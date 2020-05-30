using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.Interfaces;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate
{
    public interface IStatusRepository : IRepository<Status>
    {
        List<Status> FindAll();

        Task<Status> FindAsync(Guid statusId);

        Status Add(Status status);

        void Delete(Status status);
    }
}