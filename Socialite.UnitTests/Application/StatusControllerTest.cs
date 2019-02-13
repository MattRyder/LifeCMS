using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Infrastructure.DTO;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Controllers;
using Socialite.WebAPI.Queries.Status;
using Xunit;
using Socialite.WebAPI.Application.Commands.Status;
using System.Net;
using System.Threading;

namespace Socialite.UnitTests.Application
{
    public class StatusControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IStatusRepository> _statusRepository;
        private readonly Mock<IStatusQueries> _statusQueries;

        public StatusControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _statusRepository = new Mock<IStatusRepository>();
            _statusQueries = new Mock<IStatusQueries>();
        }

        [Fact]
        public async void Controller_ReturnsIndex()
        {
            var statusList = StatusFactory.CreateList();

            IEnumerable<StatusDTO> statusDTOList = statusList.ConvertAll<StatusDTO>(s => StatusDTO.FromModel(s));

            _statusQueries.Setup(sq => sq.FindAllAsync()).Returns(Task.FromResult(statusDTOList));

            var controller = new StatusesController(_mediator.Object, _statusRepository.Object, _statusQueries.Object);

            var actionResult = await controller.Get() as OkObjectResult;

            var resultValue = actionResult.Value as IEnumerable<StatusDTO>;

            Assert.NotNull(actionResult);

            Assert.NotNull(resultValue);

            Assert.Equal((int)System.Net.HttpStatusCode.OK, actionResult.StatusCode.GetValueOrDefault());

            Assert.Equal(statusList.Count, resultValue.Count());
        }

        [Fact]
        public async void Controller_GivenAnId_ReturnsStatus()
        {
            var expectedStatusId = 1;

            var status = StatusDTO.FromModel(StatusFactory.Create());

            status.Id = expectedStatusId;

            _statusQueries.Setup(sq => sq.FindStatus(expectedStatusId)).Returns(Task.FromResult(status));

            var controller = new StatusesController(_mediator.Object, _statusRepository.Object, _statusQueries.Object);

            var actionResult = await controller.Get(expectedStatusId) as OkObjectResult;

            var resultObject = actionResult.Value as StatusDTO;

            Assert.NotNull(actionResult);

            Assert.NotNull(resultObject);

            Assert.Equal(status, resultObject);

            Assert.NotNull(actionResult);
        }

        [Fact]
        public async void Controller__GivenStatusBody_CreatesStatus_WhenValid()
        {
            var status = StatusFactory.Create();

            _mediator.Setup(m => m.Send(It.IsAny<CreateStatusCommand>(), new CancellationToken())).Returns(Task.FromResult(true));

            var controller = new StatusesController(_mediator.Object, _statusRepository.Object, _statusQueries.Object);

            var actionResult = await controller.Post(status) as OkResult;

            Assert.Equal((int) HttpStatusCode.OK, actionResult.StatusCode);
        }

        [Fact]
        public async void Controller_GivenValidId_DeletesStatus_ReturnsOk()
        {
            _mediator.Setup(m => m.Send(It.IsAny<DeleteStatusCommand>(), default(CancellationToken))).Returns(Task.FromResult(DeleteStatusCommandResult.Success));

            var controller = new StatusesController(_mediator.Object, _statusRepository.Object, _statusQueries.Object);

            var actionResult = await controller.Delete(1) as OkResult;

            Assert.Equal((int) HttpStatusCode.OK, actionResult.StatusCode);
        }

        [Fact]
        public async void Controller_GivenInvalidId_DeletesStatus_ReturnsNotFound()
        {
            _mediator.Setup(m => m.Send(It.IsAny<DeleteStatusCommand>(), default(CancellationToken))).Returns(Task.FromResult(DeleteStatusCommandResult.NotFound));

            var controller = new StatusesController(_mediator.Object, _statusRepository.Object, _statusQueries.Object);

            var actionResult = await controller.Delete(1) as NotFoundResult;

            Assert.Equal((int) HttpStatusCode.NotFound, actionResult.StatusCode);
        }
    }
}