using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.Exceptions;
using Socialite.Infrastructure.DTO;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Posts;
using Socialite.WebAPI.Application.Enums;
using Socialite.WebAPI.Controllers;
using Socialite.WebAPI.Queries.Posts;
using Xunit;

namespace Socialite.UnitTests.Controllers
{
    public class PostsControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IPostQueries> _postQueriesMock;

        public PostsControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _postQueriesMock = new Mock<IPostQueries>();
        }

        [Fact]
        public async void Get_ReturnsOk()
        {
            IEnumerable<PostViewModel> postList = PostFactory.CreateList().ToList().ConvertAll<PostViewModel>(p => PostViewModel.FromModel(p));

            _postQueriesMock.Setup(x => x.FindAllAsync()).Returns(Task.FromResult(postList));

            var controller = new PostsController(_mediatorMock.Object, _postQueriesMock.Object);

            var result = await controller.GetPosts() as OkObjectResult;

            var resultValue = result.Value as IEnumerable<PostViewModel>;

            Assert.NotNull(result);

            Assert.NotNull(resultValue);

            Assert.Equal(postList, resultValue);
        }

        [Fact]
        public async void Get_ReturnsOk_GivenValidId()
        {
            var post = PostViewModel.FromModel(PostFactory.Create());

            _postQueriesMock.Setup(x => x.FindAsync(post.Id)).Returns(Task.FromResult(post));

            var controller = new PostsController(_mediatorMock.Object, _postQueriesMock.Object);

            var result = await controller.GetPost(post.Id) as OkObjectResult;

            var resultValue = result.Value as PostViewModel;

            Assert.NotNull(result);

            Assert.Equal(post, resultValue);
        }

        [Fact]
        public async void Get_ReturnsNotFound_GivenInvalidId()
        {
            var invalidId = 1;

            _postQueriesMock.Setup(x => x.FindAsync(invalidId)).Throws(new KeyNotFoundException());

            var controller = new PostsController(_mediatorMock.Object, _postQueriesMock.Object);

            var result = await controller.GetPost(invalidId) as NotFoundResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async void Post_ReturnsOk_GivenValidBody()
        {
            var post = PostFactory.Create();

            var createPostCmd = new CreatePostCommand(post.Title, post.Text);

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreatePostCommand>(), default(CancellationToken))).Returns(Task.FromResult(true));

            var controller = new PostsController(_mediatorMock.Object, _postQueriesMock.Object);

            var result = await controller.CreatePost(createPostCmd) as OkResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void Post_ReturnsBadRequest_GivenInvalidBody()
        {
            var createPostCmd = new CreatePostCommand(string.Empty, string.Empty);

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreatePostCommand>(), default(CancellationToken))).Returns(Task.FromResult(false));

            var controller = new PostsController(_mediatorMock.Object, _postQueriesMock.Object);

            var result = await controller.CreatePost(createPostCmd) as BadRequestObjectResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void Delete_ReturnsOk_GivenValidId()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeletePostCommand>(), default(CancellationToken))).Returns(Task.FromResult(DeleteCommandResult.Success));

            var controller = new PostsController(_mediatorMock.Object, _postQueriesMock.Object);

            var result = await controller.DeletePost(1) as OkResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void Delete_ReturnsNotFound_GivenInvalidId()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeletePostCommand>(), default(CancellationToken))).Returns(Task.FromResult(DeleteCommandResult.NotFound));

            var controller = new PostsController(_mediatorMock.Object, _postQueriesMock.Object);

            var result = await controller.DeletePost(1) as NotFoundResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);

        }

        [Fact]
        public async void Delete_ReturnsBadRequest_WhenDeleteCommandFails()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeletePostCommand>(), default(CancellationToken))).Returns(Task.FromResult(DeleteCommandResult.Failure));

            var controller = new PostsController(_mediatorMock.Object, _postQueriesMock.Object);

            var result = await controller.DeletePost(1) as BadRequestResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void Publish_ReturnsOk_GivenValidCommand()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<PublishPostCommand>(), default(CancellationToken))).Returns(Task.FromResult(true));

            var controller = new PostsController(_mediatorMock.Object, _postQueriesMock.Object);

            var result = await controller.PublishPost(1) as OkResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void Publish_ReturnsBadRequest_GivenInvalidCommand()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<PublishPostCommand>(), default(CancellationToken))).Returns(Task.FromResult(false));

            var controller = new PostsController(_mediatorMock.Object, _postQueriesMock.Object);

            var result = await controller.PublishPost(1) as BadRequestObjectResult;

            var resultValue = result.Value as PublishPostCommand;

            Assert.NotNull(result);

            Assert.NotNull(resultValue);

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}