import { push } from 'connected-react-router';
import api from '../AuthenticationApi';
import { createUrlWithQueryString } from '../../QueryString';

export const PERFORM_REGISTRATION_BEGIN = "PERFORM_REGISTRATION_BEGIN";
export const PERFORM_REGISTRATION_SUCCESS = "PERFORM_REGISTRATION_SUCCESS";
export const PERFORM_REGISTRATION_FAILURE = "PERFORM_REGISTRATION_FAILURE";

export const performRegistrationBegin = () => ({
    type: PERFORM_REGISTRATION_BEGIN,
});

export const performRegistrationSuccess = (response) => ({
    type: PERFORM_REGISTRATION_SUCCESS,
    payload: { response },
})

export const performRegistrationFailure = (errors) => ({
    type: PERFORM_REGISTRATION_FAILURE,
    payload: { errors },
});

export const performRegistration = (registrationFormBody, returnUrl) => async (dispatch) => {
    dispatch(performRegistrationBegin());

    try {
        await api.performRegistration(registrationFormBody);

        dispatch(performRegistrationSuccess());

        const loginRedirectUrl = createUrlWithQueryString('/accounts/login', {
            returnUrl
        });

        dispatch(() => push(loginRedirectUrl));
    } catch (errorResponse) {
        const { response: { data: { errors } } } = errorResponse;

        dispatch(performRegistrationFailure(errors));
    }
}