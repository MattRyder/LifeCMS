using System.Collections.Generic;

namespace Socialite.WebAPI.Application.Queries.Users
{
    public class UserViewModel : ValueObject
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

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