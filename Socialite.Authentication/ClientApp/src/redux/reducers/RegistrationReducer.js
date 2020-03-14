import {
    PERFORM_REGISTRATION_BEGIN,
    PERFORM_REGISTRATION_SUCCESS,
    PERFORM_REGISTRATION_FAILURE,
} from '../actions/RegistrationActions';

const InitialState = {
    userId: '',
    errors: [],
    isLoading: true,
};

export default (state = InitialState, action) => {
    switch(action.type) {
        case PERFORM_REGISTRATION_BEGIN:
            return {
                ...state,
                isLoading: true,
                userId: '',
                errors: [],
            };
        case PERFORM_REGISTRATION_SUCCESS:
            return {
                ...state,
                isLoading: false,
                userId: action.payload.userId,
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
