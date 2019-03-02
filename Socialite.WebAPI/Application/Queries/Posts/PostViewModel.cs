using System;
using System.Collections.Generic;
using System.Globalization;
using Socialite.Infrastructure.DTO;

namespace Socialite.WebAPI.Queries.Posts
{
    public class PostViewModel : ValueObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
        public string Text { get; set; }
        public string CreatedAt { get; set; }

        public static PostViewModel FromModel(PostDTO postDTO)
        {
            return new PostViewModel()
            {
                Id = postDTO.Id,
                State = postDTO.State.Name,
                Title = postDTO.Title,
                Text = postDTO.Text,
                CreatedAt = postDTO.CreatedAt.ToString("o", CultureInfo.InvariantCulture)
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Title;
            yield return State;
            yield return Text;
            yield return CreatedAt;
        }
    }
}