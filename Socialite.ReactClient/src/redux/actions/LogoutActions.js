import { push, replace } from 'connected-react-router';
import userManager from '../../openid/UserManager';
import Settings from '../../openid/Settings';

export const PERFORM_LOGOUT_BEGIN = 'PERFORM_LOGOUT_BEGIN';
export const PERFORM_LOGOUT_SUCCESS = 'PERFORM_LOGOUT_SUCCESS';
export const PERFORM_LOGOUT_FAILURE = 'PERFORM_LOGOUT_FAILURE';

export const performLogoutBegin = () => ({
    type: PERFORM_LOGOUT_BEGIN,
});

export const performLogoutSuccess = () => ({
    type: PERFORM_LOGOUT_SUCCESS,
});

export const performLogoutFailure = () => ({
    type: PERFORM_LOGOUT_FAILURE,
});

export const performLogout = (returnUrl = Settings.post_logout_redirect_uri) => async (dispatch) => {
    dispatch(performLogoutBegin());

    try {
        userManager.signoutRedirect();

        // window.location.href = returnUrl;
    } catch ({ message, response: { data: { errors } = {} } = {} }) {
        dispatch(performLogoutFailure(errors || [message]));
    }
};
