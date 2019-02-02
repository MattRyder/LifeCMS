using System;
using Socialite.Domain.Common;
using Socialite.Domain.Interfaces;

namespace Socialite.Domain.AggregateModels.StatusAggregate
{
    public class Status : BaseEntity, IAggregateRoot
    {
        public string Mood { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
