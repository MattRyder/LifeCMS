import axios from 'axios';

export default class Api {
    static performRegistration(registrationParams) {
        return axios.post("/api/v1/accounts", registrationParams);
    }

    static performLogin(loginParams) {
        return axios.post("/api/v1/accounts/login", loginParams);
    }
}