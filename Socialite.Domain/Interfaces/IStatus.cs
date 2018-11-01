using System;

namespace Socialite.Domain.Interfaces
{
    public interface IStatus
    {
        char Mood { get; set; }

        string Text { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}
