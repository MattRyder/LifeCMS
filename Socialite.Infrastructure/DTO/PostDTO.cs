using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Socialite.Domain.AggregateModels.PostAggregate;

namespace Socialite.Infrastructure.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public PostState State { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}