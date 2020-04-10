import axios from 'axios';
import camelcaseKeys from 'camelcase-keys';


function getAxiosInstance() {
    const instance = axios.create();

    instance.interceptors.response.use((response) => {
        response.data = camelcaseKeys(response.data);

        return response;
    });

    return instance;
}

export default class SocialiteApi {
    constructor(backendHost, accessToken) {
        if (typeof this.backendHost === 'undefined') {
            // throw new Error('Backend Host must be provided.');
        }

        this.backendHost = backendHost;

        this.accessToken = accessToken;
    }

    get(route) {
        return getAxiosInstance().get(`${this.backendHost}/${route}`, {
            timeout: 2500,
            responseType: 'json',
            headers: { Authorization: `Bearer ${this.accessToken}` },
        });
    }

    post(route, params) {
        return getAxiosInstance().post(`${this.backendHost}/${route}`, params, {
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

    createPost(postParams) {
        return this.post('posts', postParams);
    }
}

export function getSocialiteApi(accessToken) {
    return new SocialiteApi(
        process.env.REACT_APP_API_HOST,
        accessToken,
    );
}
