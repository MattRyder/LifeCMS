import { push } from 'connected-react-router';
import Api from 'redux/AuthenticationApi';

export const REQUEST_PASSWORD_RESET_SUCCESS = 'REQUEST_PASSWORD_RESET_SUCCESS';
export const REQUEST_PASSWORD_RESET_FAILURE = 'REQUEST_PASSWORD_RESET_FAILURE';

export const RESET_PASSWORD_SUCCESS = 'RESET_PASSWORD_SUCCESS';
export const RESET_PASSWORD_FAILURE = 'RESET_PASSWORD_FAILURE';

export const resetPasswordSuccess = () => ({
    type: RESET_PASSWORD_SUCCESS,
});

export const resetPasswordFailure = (error) => ({
    type: RESET_PASSWORD_FAILURE,
    payload: { error },
});

export const requestPasswordResetSuccess = () => ({
    type: REQUEST_PASSWORD_RESET_SUCCESS,
});

export const requestPasswordResetFailure = (error) => ({
    type: REQUEST_PASSWORD_RESET_FAILURE,
    payload: { error },
});

export const requestPasswordReset = (
    emailAddress, redirectTo,
) => async (dispatch) => {
    try {
        await Api.requestPasswordReset({
            email_address: emailAddress,
        });

        dispatch(requestPasswordResetSuccess());

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        dispatch(requestPasswordResetFailure(error.message));
    }
};

export const resetPassword = (
    token, email, password,
) => async (dispatch) => {
    try {
        await Api.resetPassword({
            token,
            email_address: email,
            new_password: password,
        });

        dispatch(resetPasswordSuccess());
    } catch (error) {
        dispatch(resetPasswordFailure(
            'Failed to update the password, please request another password reset link',
        ));
    }
};
