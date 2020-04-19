export default class Settings {
    static json() {
        const response = (process.env.NODE_ENV === 'production')
            ? this.readRuntimeConfig()
            : this.readEnv();

        return response;
    }

    static readEnv() {
        return {
            authority: process.env.REACT_APP_AUTHENTICATION_AUTHORITY,
            client_id: process.env.REACT_APP_AUTHENTICATION_CLIENT_ID,
            redirect_uri: process.env.REACT_APP_AUTHENTICATION_REDIRECT_URI,
            post_logout_redirect_uri: process.env.REACT_APP_AUTHENTICATION_POST_LOGOUT_REDIRECT_URI,
            response_type: 'code',
            scope: process.env.REACT_APP_AUTHENTICATION_SCOPE,
        };
    }

    static readRuntimeConfig() {
        const {
            authority,
            client_id,
            redirect_uri,
            post_logout_redirect_uri,
            response_type,
            scope,
        } = window.runtime;

        return {
            authority,
            client_id,
            redirect_uri,
            post_logout_redirect_uri,
            response_type,
            scope,
        };
    }
}
