using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using System;
using Microsoft.AspNetCore.Authorization;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Policies;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Newsletters
{
    public class UpdateNewsletterBodyCommandHandler : IRequestHandler<UpdateNewsletterBodyCommand, bool>
    {
        private readonly INewsletterRepository _newsletterRepository;

        private readonly IAuthorizationService _authorizationService;

        private readonly IUserAccessor _userAccessor;

        private readonly ILogger<DeleteNewsletterCommandHandler> _logger;

        public UpdateNewsletterBodyCommandHandler(
            INewsletterRepository newsletterRepository,
            IAuthorizationService authorizationService,
            IUserAccessor userAccessor,
            ILogger<DeleteNewsletterCommandHandler> logger
        )
        {
            _newsletterRepository = newsletterRepository;

            _authorizationService = authorizationService;

            _userAccessor = userAccessor;

            _logger = logger;
        }

        public async Task<bool> Handle(UpdateNewsletterBodyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newsletter = await _newsletterRepository.FindAsync(request.Id);

                var ownsResource = await OwnsResource(newsletter);

                if (!ownsResource)
                {
                    throw new UnauthorizedAccessException();
                }

                newsletter.UpdateNewsletterBody(request.Body);

                _newsletterRepository.Update(newsletter);

                return await _newsletterRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex) when (
                ex is NewsletterDomainException ||
                ex is UnauthorizedAccessException
            )
            {
                _logger.LogError(null, ex, ex.Message);

                return false;
            }
        }

        private async Task<bool> OwnsResource(Newsletter newsletter)
        {
            var ownsResource = await _authorizationService.AuthorizeAsync(
                _userAccessor.User,
                newsletter,
                UserOwnsResourcePolicy.Name
            );

            return ownsResource.Succeeded;
        }
    }
}