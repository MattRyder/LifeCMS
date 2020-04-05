import axios from 'axios';

export default class SocialiteApi {
    constructor(backendHost, accessToken) {
        if (typeof this.backendHost === 'undefined') {
            // throw new Error('Backend Host must be provided.');
        }

        this.backendHost = backendHost;

        this.accessToken = accessToken;
    }

    get(route) {
        return axios.get(`${this.backendHost}/${route}`, {
            timeout: 2500,
            responseType: 'json',
            headers: { Authorization: `Bearer ${this.accessToken}` },
        });
    }

    post(route, params) {
        return axios.post(`${this.backendHost}/${route}`, params, {
            headers: { Authorization: `Bearer ${this.accessToken}` },
        });
    }

    getStatuses() {
        return this.get('statuses');
    }

    getPosts() {
        return this.get('posts');
    }

    createStatus(statusParams) {
        return this.post('statuses', statusParams);
    }
}
