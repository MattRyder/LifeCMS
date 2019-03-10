using System.ComponentModel.DataAnnotations;
using MediatR;
using Socialite.Domain.AggregateModels.PostAggregate;

namespace Socialite.WebAPI.Application.Commands.Posts
{
    public class CreatePostCommand : IRequest<bool>
    {
        [Required]
        public string Title { get; private set; }

        [Required]
        public string Text { get; private set; }

        public CreatePostCommand(string title, string text)
        {
            Title = title;
            Text = text;
        }
    }
}