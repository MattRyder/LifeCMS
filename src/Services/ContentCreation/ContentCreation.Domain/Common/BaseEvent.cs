using System;
using MediatR;

namespace LifeCMS.Services.ContentCreation.Domain.Common
{
    public abstract class BaseEvent : INotification
    {
        public DateTime RaisedAt { get; protected set; } = DateTime.UtcNow;
    }
}
