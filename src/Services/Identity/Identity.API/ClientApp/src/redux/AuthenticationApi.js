import axios from 'axios';
import { createUrlWithQueryString } from '../QueryString';

export default class Api {
    static performRegistration(registrationParams) {
        return axios.post('/api/v1/accounts', registrationParams);
    }

    static performAuthentication(authenticationParams, queryParams) {
        const route = createUrlWithQueryString(
            '/api/v1/accounts/login',
            queryParams,
        );

        return axios.post(route, authenticationParams);
    }

    static requestPasswordReset(params) {
        return axios.post('/api/v1/password/requestReset', params);
    }

    static resetPassword(params) {
        return axios.post('/api/v1/password/reset', params);
    }
}
