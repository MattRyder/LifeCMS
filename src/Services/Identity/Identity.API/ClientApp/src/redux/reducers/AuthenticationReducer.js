import {
    PERFORM_AUTHENTICATION_BEGIN,
    PERFORM_AUTHENTICATION_SUCCESS,
    PERFORM_AUTHENTICATION_FAILURE,
} from '../actions/AuthenticationActions';

const InitialState = {
    authentication: {},
    errors: [],
    isLoading: false,
};

export default (state = InitialState, action) => {
    switch (action.type) {
    case PERFORM_AUTHENTICATION_BEGIN:
        return {
            ...state,
            isLoading: true,
            errors: [],
        };
    case PERFORM_AUTHENTICATION_SUCCESS:
        return {
            ...state,
            isLoading: false,
            authentication: action.payload.authentication,
        };
    case PERFORM_AUTHENTICATION_FAILURE:
        return {
            ...state,
            isLoading: false,
            errors: action.payload.errors,
        };
    default:
        return state;
    }
};
