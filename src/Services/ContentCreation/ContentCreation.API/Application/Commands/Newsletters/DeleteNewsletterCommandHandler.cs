using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Policies;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Newsletters
{
    public class DeleteNewsletterCommandHandler : IRequestHandler<DeleteNewsletterCommand, bool>
    {
        private readonly INewsletterRepository _newsletterRepository;

        private readonly IAuthorizationService _authorizationService;

        private readonly IUserAccessor _userAccessor;

        private readonly ILogger<DeleteNewsletterCommandHandler> _logger;

        public DeleteNewsletterCommandHandler(
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

        public async Task<bool> Handle(DeleteNewsletterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newsletter = await _newsletterRepository.FindAsync(request.Id);

                var ownsResource = await OwnsResource(newsletter);

                if (!ownsResource)
                {
                    throw new UnauthorizedAccessException();
                }

                _newsletterRepository.Delete(newsletter);

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