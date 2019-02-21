using System;
using System.Globalization;
using Socialite.Infrastructure.DTO;

namespace Socialite.WebAPI.Queries.Posts
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string Text { get; set; }
        public string CreatedAt { get; set; }

        public static PostViewModel FromModel(PostDTO postDTO)
        {
            return new PostViewModel()
            {
                Id = postDTO.Id,
                State = postDTO.State.Name,
                Text = postDTO.Text,
                CreatedAt = postDTO.CreatedAt.ToString("o", CultureInfo.InvariantCulture)
            };
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherViewModel = obj as PostViewModel;

            return Id.Equals(otherViewModel.Id) &&
                    State.Equals(otherViewModel.State) &&
                    Text.Equals(otherViewModel.Text) &&
                    CreatedAt.Equals(otherViewModel.CreatedAt);
        }

        public override int GetHashCode()
        {
            var hashCode = 13;
            var prime = 397;

            hashCode = (hashCode * prime) ^ Id;

            var stateHash = !string.IsNullOrEmpty(State) ? State.GetHashCode() : 0;

            hashCode = (hashCode * prime) ^ stateHash;

            var textHash = !string.IsNullOrEmpty(Text) ? Text.GetHashCode() : 0;

            hashCode = (hashCode * prime) ^ textHash;

            hashCode = (hashCode * prime) ^ CreatedAt.GetHashCode();

            return hashCode;
        }
    }
}