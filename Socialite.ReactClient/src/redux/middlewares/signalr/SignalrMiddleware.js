import { USER_FOUND } from 'redux-oidc';
import CreateHubConnection from './CreateHubConnection';
import RuntimeConfiguration from './RuntimeConfiguration';

const onLogin = (hubUrl, accessToken) => {
    CreateHubConnection(hubUrl, accessToken);
};

export default () => (next) => (action) => {
    switch (action.type) {
    case USER_FOUND: {
        onLogin(
            RuntimeConfiguration.json().websocket_host,
            action.payload.access_token,
        );

        break;
    }
    default:
        break;
    }

    return next(action);
};
