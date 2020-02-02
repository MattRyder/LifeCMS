using System;
using System.Collections.Generic;
using Socialite.Domain.AggregateModels.StatusAggregate;

namespace Socialite.WebAPI.Application.Queries.Statuses
{
    public class StatusViewModel : ValueObject
    {
        public Guid Id { get; set; }

        public string Mood { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Mood;
            yield return Text;
            yield return CreatedAt;
        }

        public static StatusViewModel FromModel(Status status)
        {
            return new StatusViewModel
            {
                Id = status.Id,
                Mood = status.Mood,
                Text = status.Text,
                CreatedAt = status.CreatedAt
            };
        }
    }
}