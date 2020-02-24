import { push } from 'connected-react-router';
import api from '../AuthenticationApi';
import { createUrlWithQueryString } from '../../QueryString';

export const PERFORM_REGISTRATION_BEGIN = "PERFORM_REGISTRATION_BEGIN";
export const PERFORM_REGISTRATION_SUCCESS = "PERFORM_REGISTRATION_SUCCESS";
export const PERFORM_REGISTRATION_FAILURE = "PERFORM_REGISTRATION_FAILURE";

export const performRegistrationBegin = () => ({
    type: PERFORM_REGISTRATION_BEGIN,
});

export const performRegistrationSuccess = (userId) => ({
    type: PERFORM_REGISTRATION_SUCCESS,
    payload: { userId },
})

export const performRegistrationFailure = (errors) => ({
    type: PERFORM_REGISTRATION_FAILURE,
    payload: { errors },
});

export const performRegistration = (registrationFormBody, returnUrl) => async (dispatch) => {
    dispatch(performRegistrationBegin());

    try {
        const { data: { data: { id } = {} } = {} } = await api.performRegistration(registrationFormBody);

        dispatch(performRegistrationSuccess(id));

        const loginRedirectUrl = createUrlWithQueryString('/accounts/login', {
            returnUrl
        });

        setTimeout(() => window.location.href = loginRedirectUrl, 1000 * 3);
    } catch ({ message, response: { data: { errors } = {} } = {} }) {
        dispatch(performRegistrationFailure(errors || [message]));
    }
}