import {
    PERFORM_REGISTRATION_BEGIN,
    PERFORM_REGISTRATION_SUCCESS,
    PERFORM_REGISTRATION_FAILURE,
} from '../actions/RegistrationActions';

const InitialState = {
    registration: {},
    errors: [],
    isLoading: true,
};

export default (state = InitialState, action) => {
    switch(action.type) {
        case PERFORM_REGISTRATION_BEGIN:
            return {
                ...state,
                isLoading: true,
                errors: [],
            };
        case PERFORM_REGISTRATION_SUCCESS:
            return {
                ...state,
                isLoading: false,
                registration: action.payload,
                errors: []
            };
        case PERFORM_REGISTRATION_FAILURE:
            return {
                ...state,
                isLoading: false,
                errors: action.payload.errors,
            };
        default:
            return state;
    }
};
