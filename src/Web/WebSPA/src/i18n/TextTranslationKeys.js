import {
    Audience, Common, Confirm, Post,
    Subscriber, Campaign, Template,
    UserProfile, HomeView, NavigationMenu,
    AudienceView, CampaignView, NewsletterView, UserProfileView,
} from './keys';

export default {
    common: Common,
    confirm: Confirm,
    post: Post,
    audience: Audience,
    subscriber: Subscriber,
    campaign: Campaign,
    template: Template,
    fileUploader: {
        cta: 'fileUploaderCta',
    },
    editableText: {
        placeholder: 'editableTextPlaceholder',
    },
    userProfile: UserProfile,
    homeView: HomeView,
    navigationMenu: NavigationMenu,
    audienceView: AudienceView,
    campaignView: CampaignView,
    newsletterView: NewsletterView,
    subscriberConfirmView: {
        title: 'subscriberConfirmViewTitle',
        text: 'subscriberConfirmViewText',
        loadingText: 'subscriberConfirmViewLoadingText',
        warningText: 'subscriberConfirmViewWarningText',
    },
    userProfileView: UserProfileView,
};
