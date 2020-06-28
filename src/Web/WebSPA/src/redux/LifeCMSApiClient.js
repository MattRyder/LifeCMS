import axios from 'axios';
import camelcaseKeys from 'camelcase-keys';

function getAxiosInstance(accessToken) {
    const instance = axios.create();

    instance.interceptors.response.use((response) => {
        response.data = camelcaseKeys(response.data);

        return response;
    });

    instance.defaults.headers = {
        Authorization: `Bearer ${accessToken}`,
    };

    return instance;
}

export default class LifeCMSApiClient {
    constructor(backendHost, accessToken) {
        this.backendHost = backendHost;

        this.accessToken = accessToken;
    }

    get(route) {
        return getAxiosInstance(this.accessToken)
            .get(`${this.backendHost}/${route}`, {
                timeout: 2500,
                responseType: 'json',
            });
    }

    post(route, params) {
        return getAxiosInstance(this.accessToken)
            .post(`${this.backendHost}/${route}`, params);
    }

    delete(route) {
        return getAxiosInstance(this.accessToken)
            .delete(`${this.backendHost}/${route}`);
    }
}
