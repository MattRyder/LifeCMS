import WebApiConfiguration from './WebApiConfiguration';
import LifeCMSApiClient from './LifeCMSApiClient';

export default class LifeCMSApi {
    constructor(backendHost, accessToken) {
        this.client = new LifeCMSApiClient(
            backendHost,
            accessToken,
        );
    }

    getPosts() {
        return this.client.get('posts');
    }

    createPost(postParams) {
        return this.client.post('posts', postParams);
    }

    getUserProfiles() {
        return this.client.get('userProfiles');
    }

    createUserProfile(userProfileParams) {
        return this.client.post('userProfiles', userProfileParams);
    }

    editUserProfile(params) {
        return this.client.put('userProfiles', params);
    }

    deleteUserProfile(userProfileId) {
        return this.client.delete(`userProfiles/${userProfileId}`);
    }

    getNewsletters(userId) {
        return this.client.get(`users/${userId}/newsletters`);
    }

    createNewsletter(userId, newsletterParams) {
        return this.client.post(`users/${userId}/newsletters`, newsletterParams);
    }

    editNewsletterBody(userId, newsletterId, body) {
        return this.client.post(
            `users/${userId}/newsletters/${newsletterId}/updateNewsletterBody`,
            { id: newsletterId, body },
        );
    }

    deleteNewsletter(userId, newsletterId) {
        return this.client.delete(`users/${userId}/newsletters/${newsletterId}`);
    }

    getCampaigns(userId) {
        return this.client.get(`users/${userId}/campaigns`);
    }

    createCampaign(params) {
        return this.client.post('campaigns', params);
    }

    updateCampaignSubject(campaignId, params) {
        return this.client.post(
            `campaigns/${campaignId}/updateSubject`,
            params,
        );
    }

    updateCampaignName(campaignId, params) {
        return this.client.post(
            `campaigns/${campaignId}/updateName`,
            params,
        );
    }

    deleteCampaign(campaignId) {
        return this.client.delete(`campaigns/${campaignId}`);
    }

    createPresignUrl(params) {
        return this.client.post('files', params);
    }

    createFileUri(fileUrn) {
        return this.client.post(`files/${fileUrn}`);
    }

    getAudiences() {
        return this.client.get('audiences');
    }

    createAudience(params) {
        return this.client.post('audiences', params);
    }

    deleteAudience(audienceId) {
        return this.client.delete(`audiences/${audienceId}`);
    }

    addSubscriber(params) {
        return this.client.post('audiences/addSubscriber', params);
    }

    confirmSubscriber(params) {
        return this.client.post('subscribers/confirm', params);
    }

    updateAudienceName(audienceId, params) {
        return this.client.post(`audiences/${audienceId}/updateName`, params);
    }

    getAudienceSubscribers(audienceId) {
        return this.client.get(`audiences/${audienceId}/subscribers`);
    }
}

export function getLifeCMSApi(accessToken) {
    return new LifeCMSApi(
        WebApiConfiguration.json().api_host,
        accessToken,
    );
}
