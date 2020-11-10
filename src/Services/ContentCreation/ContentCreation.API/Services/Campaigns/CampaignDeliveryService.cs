using System;
using System.Threading.Tasks;
using LifeCMS.EventBus.Common.Interfaces;
using LifeCMS.EventBus.IntegrationEvents.Email;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using Microsoft.Extensions.Configuration;

namespace LifeCMS.Services.ContentCreation.API.Services.Campaigns
{
    public class CampaignDeliveryService : ICampaignDeliveryService
    {
        private readonly ICampaignRepository _campaignRepository;

        private readonly INewsletterRepository _newsletterRepository;

        private readonly IEventBus _eventBus;

        private readonly IConfiguration _configuration;

        public CampaignDeliveryService(
            ICampaignRepository campaignRepository,
            INewsletterRepository newsletterRepository,
            IEventBus eventBus,
            IConfiguration configuration
        )
        {
            _campaignRepository = campaignRepository;

            _newsletterRepository = newsletterRepository;

            _eventBus = eventBus;

            _configuration = configuration;
        }

        public async void Execute(Guid campaignId)
        {
            var campaign = await FindCampaign(campaignId);

            var newsletter = await FindNewsletter(
                campaign.NewsletterTemplateId
            );

            var sendEmailEvent = CreateCampaignSendEmailEvent(
                campaign,
                newsletter
            );

            _eventBus.Publish(sendEmailEvent);
        }

        private SendEmailEvent CreateCampaignSendEmailEvent(
            Campaign campaign,
            Newsletter newsletter
        )
        {
            return new SendEmailEvent
            {
                From = _configuration["ContentCreation:Email:FromEmailAddress"],
                To = new[] { "matt@cubicle.dev" },
                Subject = campaign.Subject.SubjectLineText,
                Body = newsletter.Body.Html,
                IsBodyHtml = true,
                DeliverAt = DateTime.Now,
            };
        }

        private async Task<Campaign> FindCampaign(Guid campaignId)
        {
            return await _campaignRepository.FindAsync(campaignId);
        }

        private async Task<Newsletter> FindNewsletter(Guid newsletterId)
        {
            return await _newsletterRepository.FindAsync(newsletterId);
        }
    }
}
