using System;
using System.Threading;
using System.Threading.Tasks;
using EasyCrypto;
using LifeCMS.Services.ContentCreation.API.Services.Audiences;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences
{
    public class AddSubscriberCommandHandler
        : IRequestHandler<AddSubscriberCommand, BasicResponse>
    {
        private readonly IAudienceRepository _audienceRepository;

        private readonly IAudienceLookupService _audienceLookupService;

        private readonly ILogger<AddSubscriberCommandHandler> _logger;

        private readonly ISubscriberEmailService _subscriberEmailService;

        public AddSubscriberCommandHandler(
            IAudienceRepository audienceRepository,
            IAudienceLookupService audienceLookupService,
            ILogger<AddSubscriberCommandHandler> logger,
            ISubscriberEmailService subscriberEmailService)
        {
            _audienceRepository = audienceRepository;

            _audienceLookupService = audienceLookupService;

            _logger = logger;

            _subscriberEmailService = subscriberEmailService;
        }

        public async Task<BasicResponse> Handle(
            AddSubscriberCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var audience = await _audienceLookupService.FindAudienceAsync(
                    request.AudienceId);

                var subscriber = await CreateSubscriberAsync(
                    audience,
                    request.Name,
                    request.EmailAddress);

                if (!request.ConsentConfirmed)
                {
                    _subscriberEmailService.SendEmail(
                        audience.Id,
                        subscriber.EmailAddress,
                        subscriber.Name,
                        subscriber.SubscriberToken);
                }

                return new BasicResponse()
                {
                    Success = true
                };
            }
            catch (Exception ex) when (
                ex is AudienceLookupServiceException ||
                ex is AudienceDomainException
            )
            {
                _logger.LogError(ex, ex.Message);

                return new BasicResponse()
                {
                    Success = false,
                    Errors = new[] { ex.Message }
                };
            }
        }

        private async Task<Subscriber> CreateSubscriberAsync(
            Audience audience,
            string name,
            EmailAddress emailAddress)
        {
            var token = GenerateToken();

            var subscriber = audience.AddSubscriber(name, emailAddress, token);

            await _audienceRepository.UnitOfWork.SaveEntitiesAsync();

            return subscriber;
        }

        private string GenerateToken(int tokenLength = 16)
        {
            var generator = new TokenGenerator();

            return generator.GenerateToken(tokenLength);
        }
    }
}
