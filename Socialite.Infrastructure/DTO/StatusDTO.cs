using Socialite.Domain.Entities;
using Socialite.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Socialite.Infrastructure.DTO
{
    public class StatusDTO : IStatus
    {
        public int Id { get; set; }

        public char Mood { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public static StatusDTO FromModel(Status status)
        {
            return new StatusDTO()
            {
                Id = status.Id,
                Mood = status.Mood,
                Text = status.Text,
                CreatedAt = status.CreatedAt,
                UpdatedAt = status.UpdatedAt
            };
        }
    }
}
