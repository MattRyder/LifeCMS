using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Policies;
using LifeCMS.Services.ContentCreation.API.Services.Lookup;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Newsletters
{
    public class DeleteNewsletterCommandHandler : IRequestHandler<DeleteNewsletterCommand, bool>
    {
        private readonly INewsletterRepository _newsletterRepository;

        private readonly ILookupService<Newsletter> _lookupService;

        private readonly ILogger<DeleteNewsletterCommandHandler> _logger;

        public DeleteNewsletterCommandHandler(
            INewsletterRepository newsletterRepository,
            ILookupService<Newsletter> lookupService,
            ILogger<DeleteNewsletterCommandHandler> logger
        )
        {
            _newsletterRepository = newsletterRepository;

            _lookupService = lookupService;

            _logger = logger;
        }

        public async Task<bool> Handle(
            DeleteNewsletterCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var newsletter = await _lookupService
                    .FindEntityByIdAsync(request.Id);

                _newsletterRepository.Delete(newsletter);

                return await _newsletterRepository
                    .UnitOfWork
                    .SaveEntitiesAsync();
            }
            catch (LookupServiceException ex)
            {
                _logger.LogError(null, ex, ex.Message);

                return false;
            }
        }
    }
}
