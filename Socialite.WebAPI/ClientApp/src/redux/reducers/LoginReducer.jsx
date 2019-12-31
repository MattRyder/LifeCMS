import {
    PERFORM_LOGIN_BEGIN,
    PERFORM_LOGIN_SUCCESS,
    PERFORM_LOGIN_FAILURE,
} from '../actions/LoginActions';

const InitialState = {
    login: {},
    error: null,
    loading: true,
};

export default (state = InitialState, action) => {
    switch(action.type) {
        case PERFORM_LOGIN_BEGIN:
            return {
                ...state,
                loading: true,
                error: null,
            };
        case PERFORM_LOGIN_SUCCESS:
            return {
                ...state,
                loading: false,
                login: action.payload,
            };
        case PERFORM_LOGIN_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.payload,
            };
        default:
            return state;
    }
};
