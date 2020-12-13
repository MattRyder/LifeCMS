import { createSelector, ORM } from 'redux-orm';
import {
    Campaign, NewsletterTemplate, User, UserProfile,
} from './models';

const orm = new ORM({
    stateSelector: (state) => state.orm,
});

orm.register(Campaign, User, UserProfile, NewsletterTemplate);

const findAssociationById = (selector, id) => createSelector(
    selector,
    (records) => records.find((record) => record.id === id),
);

export const findUserCampaigns = createSelector(orm.User.campaigns);

export const findUserUserProfiles = createSelector(orm.User.userProfiles);

export const findUserNewsletterTemplates = createSelector(orm.User.newsletterTemplates);

export const findUserProfile = (userProfileId) => findAssociationById(
    orm.User.userProfiles, userProfileId,
);

export const findUserCampaign = (campaignId) => findAssociationById(
    orm.User.campaigns,
    campaignId,
);

export const findUserNewsletterTemplate = (newsletterTemplateId) => findAssociationById(
    orm.User.newsletterTemplates,
    newsletterTemplateId,
);

export default orm;
