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

    getUserProfiles(userId) {
        return this.client.get(`users/${userId}/userProfiles`);
    }

    createUserProfile(userId, userProfileParams) {
        return this.client.post(`users/${userId}/userProfiles`, userProfileParams);
    }

    deleteUserProfile(userId, userProfileId) {
        return this.client.delete(`users/${userId}/userProfiles/${userProfileId}`);
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
}

export function getLifeCMSApi(accessToken) {
    return new LifeCMSApi(
        WebApiConfiguration.json().api_host,
        accessToken,
    );
}
