using MediatR;
using Socialite.Domain.AggregateModels.PostAggregate;

namespace Socialite.WebAPI.Application.Commands.Posts
{
    public class CreatePostCommand : IRequest<bool>
    {
        public Post Post { get; private set; }

        public CreatePostCommand(Post post)
        {
            this.Post = post;
        }
    }
}