using System;
using Socialite.Domain.Common;
using Socialite.Domain.Interfaces;

namespace Socialite.Domain.Entities
{
    public class Status : BaseEntity, IStatus
    {
        public char Mood { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
