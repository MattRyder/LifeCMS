using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Socialite.Infrastructure.DTO
{
    public class StatusDTO
    {
        public int Id { get; set; }

        [Required]
        public String Mood { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public static StatusDTO FromModel(Status status)
        {
            return new StatusDTO()
            {
                Id = status.Id,
                Mood = status.Mood,
                Text = status.Text,
                CreatedAt = status.CreatedAt
            };
        }
    }
}
