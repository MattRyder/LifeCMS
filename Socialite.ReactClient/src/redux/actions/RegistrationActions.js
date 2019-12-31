import api from '../AuthenticationApi';

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

export const performRegistrationFailure = (error) => ({
    type: PERFORM_REGISTRATION_FAILURE,
    payload: { error },
});

export const performRegistration = (registrationFormBody) => (dispatch) => {
    dispatch(performRegistrationBegin());

    api.performRegistration(registrationFormBody)
        .then((response) => dispatch(performRegistrationSuccess(response.data)))
        .catch((error) => console.log(JSON.stringify(error)));
}