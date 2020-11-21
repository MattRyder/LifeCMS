import { push } from 'connected-react-router';
import { error as toastError } from 'react-toastify-redux';

// eslint-disable-next-line import/prefer-default-export
export const redirectWithError = (redirectTo, errorMessage) => (dispatch) => {
    dispatch(toastError(errorMessage));

    dispatch(push(redirectTo));
};
