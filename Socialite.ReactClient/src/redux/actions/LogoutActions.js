import userManager from '../../openid/UserManager';

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

export const performLogout = () => async (dispatch) => {
    dispatch(performLogoutBegin());

    try {
        userManager.signoutRedirect();
    } catch ({ message, response: { data: { errors } = {} } = {} }) {
        dispatch(performLogoutFailure(errors || [message]));
    }
};
