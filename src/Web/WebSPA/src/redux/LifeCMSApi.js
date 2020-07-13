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

    deleteNewsletter(userId, newsletterId) {
        return this.client.delete(`users/${userId}/newsletters/${newsletterId}`);
    }
}

export function getLifeCMSApi(accessToken) {
    return new LifeCMSApi(
        WebApiConfiguration.json().api_host,
        accessToken,
    );
}
