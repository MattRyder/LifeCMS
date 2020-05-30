using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Posts;
using LifeCMS.Services.ContentCreation.API.Application.Enums;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Application.Commands
{
    public class DeletePostCommandHandlerTest
    {
        private readonly Mock<IPostRepository> _postRepositoryMock;

        public DeletePostCommandHandlerTest()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_GivenValidCommandAsync()
        {
            var postId = new Guid();

            var post = PostFactory.Create();

            post.Id = postId;

            var deletePostCmd = new DeletePostCommand(postId);

            _postRepositoryMock.Setup(p => p.FindAsync(postId)).Returns(new ValueTask<Post>(post));

            _postRepositoryMock.Setup(p => p.Delete(It.IsAny<Post>()));

            _postRepositoryMock.Setup(p => p.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new DeletePostCommandHandler(_postRepositoryMock.Object);

            var result = await handler.Handle(deletePostCmd, default);

            Assert.Equal(DeleteCommandResult.Success, result);
        }

        [Fact]
        public async Task Handle_ReturnsNotFound_GivenInvalidId()
        {
            var deletePostCmd = new DeletePostCommand(new Guid());

            _postRepositoryMock.Setup(p => p.FindAsync(It.IsAny<Guid>())).Returns(new ValueTask<Post>((Post)null));

            var handler = new DeletePostCommandHandler(_postRepositoryMock.Object);

            var result = await handler.Handle(deletePostCmd, default);

            Assert.Equal(DeleteCommandResult.NotFound, result);
        }
    }
}