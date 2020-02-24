import api from '../AuthenticationApi';
import { push, replace } from 'connected-react-router';
import { LOCATION_CHANGE } from 'connected-react-router';

export const PERFORM_AUTHENTICATION_BEGIN = "PERFORM_AUTHENTICATION_BEGIN";
export const PERFORM_AUTHENTICATION_SUCCESS = "PERFORM_AUTHENTICATION_SUCCESS";
export const PERFORM_AUTHENTICATION_FAILURE = "PERFORM_AUTHENTICATION_FAILURE";

export const performAuthenticationBegin = () => ({
    type: PERFORM_AUTHENTICATION_BEGIN,
});

export const performAuthenticationSuccess = (authentication) => ({
    type: PERFORM_AUTHENTICATION_SUCCESS,
    payload: { authentication },
})

export const performAuthenticationFailure = (errors) => ({
    type: PERFORM_AUTHENTICATION_FAILURE,
    payload: { errors },
});

export const performAuthentication = (authenticationParams, returnUrl) => async (dispatch) => {
    dispatch(performAuthenticationBegin());

    try {
        const { data: { data } = {}} = await api.performAuthentication(authenticationParams, {
            returnUrl
        });

        if (!data) {
            throw new Error("callback url was not present on the response.");
        }

        window.location.href = data;
    } catch ({ message, response: { data: { errors } = {}} = {}}) {
        dispatch(performAuthenticationFailure(errors || [message]));
    }
}