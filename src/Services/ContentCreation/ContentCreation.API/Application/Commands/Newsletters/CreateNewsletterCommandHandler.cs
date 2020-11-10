using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using LifeCMS.Services.ContentCreation.Domain.Exceptions;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Newsletters
{
    public class CreateNewsletterCommandHandler : IRequestHandler<CreateNewsletterCommand, bool>
    {
        private readonly INewsletterRepository _newsletterRepository;

        private readonly ILogger<CreateNewsletterCommandHandler> _logger;

        private readonly IUserAccessor _userAccessor;

        public CreateNewsletterCommandHandler(
            INewsletterRepository newsletterRepository,
            ILogger<CreateNewsletterCommandHandler> logger,
            IUserAccessor userAccessor
        )
        {
            _newsletterRepository = newsletterRepository;

            _logger = logger;

            _userAccessor = userAccessor;
        }

        public async Task<bool> Handle(CreateNewsletterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newsletter = new Newsletter(
                    _userAccessor.Id,
                    request.Name,
                    request.Body
                );

                _newsletterRepository.AddAsync(newsletter);

                return await _newsletterRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (PostDomainException ex)
            {
                _logger.LogError(ex, null);

                return false;
            }
        }
    }
}
