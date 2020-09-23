import { push } from 'connected-react-router';

export const RAISE_ERROR = 'RAISE_ERROR';

export const raiseError = (error) => ({
    type: RAISE_ERROR,
    payload: { error },
});

export const redirectToError = (redirectTo, errorMessage) => async (dispatch) => {
    dispatch(raiseError(errorMessage));

    dispatch(push(redirectTo));
};
