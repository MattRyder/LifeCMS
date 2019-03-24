using System.Collections.Generic;
using Socialite.Domain.AggregateModels.UsersAggregate;

namespace Socialite.WebAPI.Application.Queries.Users
{
    public class UserViewModel : ValueObject
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        public static UserViewModel FromModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                CreatedAt = user.CreatedAt.ToString(),
                UpdatedAt = user.UpdatedAt.ToString()
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Email;
            yield return Name;
            yield return CreatedAt;
            yield return UpdatedAt;
        }
    }
}