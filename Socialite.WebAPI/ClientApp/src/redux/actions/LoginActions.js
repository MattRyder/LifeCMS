import api from '../AuthenticationApi';

export const PERFORM_LOGIN_BEGIN = "PERFORM_LOGIN_BEGIN";
export const PERFORM_LOGIN_SUCCESS = "PERFORM_LOGIN_SUCCESS";
export const PERFORM_LOGIN_FAILURE = "PERFORM_LOGIN_FAILURE";

export const performLoginBegin = () => ({
    type: PERFORM_LOGIN_BEGIN,
});

export const performLoginSuccess = (response) => ({
    type: PERFORM_LOGIN_SUCCESS,
    payload: { response },
})

export const performLoginFailure = (error) => ({
    type: PERFORM_LOGIN_FAILURE,
    payload: { error },
});

export const performLogin = (loginFormBody) => (dispatch) => {
    dispatch(performLoginBegin());

    api.performLogin(loginFormBody)
        .then((response) => dispatch(performLoginSuccess(response.data)))
        .catch((error) => console.log(JSON.stringify(error)));
}