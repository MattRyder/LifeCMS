import api from '../AuthenticationApi';
import { push } from 'connected-react-router';

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
        const response = await api.performAuthentication(authenticationParams, { 
            returnUrl
        });
        
        debugger;
        const { data: { data } = {} } = response;


        const authenticationJson = JSON.parse(data);

        dispatch(performAuthenticationSuccess(authenticationJson));

        dispatch(push(returnUrl));
    } catch (errorResponse) {
        const { response: { data: { errors } } } = errorResponse;

        dispatch(performAuthenticationFailure(errors));
    }
}