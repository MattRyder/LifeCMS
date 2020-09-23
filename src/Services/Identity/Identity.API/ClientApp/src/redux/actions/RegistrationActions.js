import api from '../AuthenticationApi';

export const PERFORM_REGISTRATION_BEGIN = 'PERFORM_REGISTRATION_BEGIN';
export const PERFORM_REGISTRATION_SUCCESS = 'PERFORM_REGISTRATION_SUCCESS';
export const PERFORM_REGISTRATION_FAILURE = 'PERFORM_REGISTRATION_FAILURE';

export const performRegistrationBegin = () => ({
    type: PERFORM_REGISTRATION_BEGIN,
});

export const performRegistrationSuccess = (userId) => ({
    type: PERFORM_REGISTRATION_SUCCESS,
    payload: { userId },
});

export const performRegistrationFailure = (errors) => ({
    type: PERFORM_REGISTRATION_FAILURE,
    payload: { errors },
});

export const performRegistration = (
    registrationFormBody, redirectTo,
) => async (dispatch) => {
    dispatch(performRegistrationBegin());

    try {
        const {
            data: { data: { id } = {} } = {},
        } = await api.performRegistration(registrationFormBody);

        dispatch(performRegistrationSuccess(id));
    } catch ({ message, response: { data: { errors } = {} } = {} }) {
        dispatch(performRegistrationFailure(errors || [message]));
    }
};
