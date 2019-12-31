import axios from 'axios';

export default class SocialiteApi {
    constructor(backendHost, apiKey) {
        this.backendHost = backendHost;

        this.apiKey = apiKey;
    }

    get(route) {
        return axios.get(`${this.backendHost}/${route}`, {
            timeout: 2500,
            responseType: 'json',
            headers: { "Authentication": `Bearer: ${this.apiKey}` }
        })
    }

    getStatuses() {
        return this.get("statuses");
    }

    getPosts() {
        return this.get("posts");
    }
}