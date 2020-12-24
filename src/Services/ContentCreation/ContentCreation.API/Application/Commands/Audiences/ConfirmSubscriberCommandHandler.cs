using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences
{
    public class ConfirmSubscriberCommandHandler
        : IRequestHandler<ConfirmSubscriberCommand, BasicResponse>
    {
        private readonly IAudienceRepository _audienceRepository;

        private readonly IUserAccessor _userAccessor;

        public ConfirmSubscriberCommandHandler(
            IAudienceRepository audienceRepository,
            IUserAccessor userAccessor)
        {
            _audienceRepository = audienceRepository;

            _userAccessor = userAccessor;
        }

        public async Task<BasicResponse> Handle(
            ConfirmSubscriberCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var subscriber = FindSubscriber(request.SubscriberToken);

                Subscribe(subscriber);

                await _audienceRepository.UnitOfWork.SaveEntitiesAsync();

                return new BasicResponse()
                {
                    Success = true
                };
            }
            catch (AudienceDomainException ex)
            {
                return new BasicResponse()
                {
                    Success = false,
                    Errors = new[] { ex.Message }
                };
            }
        }

        private Subscriber FindSubscriber(string subscriberToken)
        {
            return _audienceRepository
                .FindSubscriberBySubscriberToken(subscriberToken);
        }

        private void Subscribe(Subscriber subscriber)
        {
            var remoteIpAddress = _userAccessor.RemoteIpAddress.ToString();

            subscriber.Subscribe(remoteIpAddress);
        }
    }
}
