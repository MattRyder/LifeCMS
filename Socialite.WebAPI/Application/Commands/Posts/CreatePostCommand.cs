using MediatR;
using Socialite.Domain.AggregateModels.PostAggregate;

namespace Socialite.WebAPI.Application.Commands.Posts
{
    public class CreatePostCommand : IRequest<bool>
    {
        public string Text { get; private set; }

        public CreatePostCommand(string text)
        {
            Text = text;
        }
    }
}