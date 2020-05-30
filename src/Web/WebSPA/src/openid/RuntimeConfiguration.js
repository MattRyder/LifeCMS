import RuntimeConfigurationLoader from '../RuntimeConfigurationLoader';

export default class UserManagerRuntimeConfiguration extends RuntimeConfigurationLoader {
    static keys() {
        return {
            authority: "REACT_APP_RUNTIME_AUTHORITY",
            client_id: "REACT_APP_RUNTIME_CLIENT_ID",
            redirect_uri: "REACT_APP_RUNTIME_REDIRECT_URI",
            post_logout_redirect_uri: "REACT_APP_RUNTIME_POST_LOGOUT_REDIRECT_URI",
            response_type: "REACT_APP_RUNTIME_RESPONSE_TYPE",
            scope: "REACT_APP_RUNTIME_SCOPE",
        };
    }
}
