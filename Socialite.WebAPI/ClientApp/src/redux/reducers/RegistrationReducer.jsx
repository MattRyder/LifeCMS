import {
    PERFORM_REGISTRATION_BEGIN,
    PERFORM_REGISTRATION_SUCCESS,
    PERFORM_REGISTRATION_FAILURE,
} from '../actions/RegistrationActions';

const InitialState = {
    registration: {},
    error: null,
    loading: true,
};

export default (state = InitialState, action) => {
    switch(action.type) {
        case PERFORM_REGISTRATION_BEGIN:
            return {
                ...state,
                loading: true,
                error: null,
            };
        case PERFORM_REGISTRATION_SUCCESS:
            return {
                ...state,
                loading: false,
                registration: action.payload,
            };
        case PERFORM_REGISTRATION_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.payload,
            };
        default:
            return state;
    }
};
