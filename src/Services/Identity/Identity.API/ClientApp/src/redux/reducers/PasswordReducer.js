import {
    REQUEST_PASSWORD_RESET_FAILURE,
    REQUEST_PASSWORD_RESET_SUCCESS,
    RESET_PASSWORD_FAILURE,
    RESET_PASSWORD_SUCCESS,
} from '../actions/PasswordActions';

const InitialState = {
    passwordRequest: {
        error: '',
        requested: false,
    },
    passwordReset: {
        error: '',
        reset: false,
    },
};

export default function PasswordReducer(state = InitialState, action) {
    switch (action.type) {
    case REQUEST_PASSWORD_RESET_FAILURE: {
        return {
            ...state,
            passwordRequest: {
                error: action.payload.error,
                requested: false,
            },
        };
    }
    case REQUEST_PASSWORD_RESET_SUCCESS: {
        return {
            ...state,
            passwordRequest: {
                error: '',
                requested: true,
            },
        };
    }
    case RESET_PASSWORD_FAILURE: {
        return {
            ...state,
            passwordReset: {
                error: action.payload.error,
                reset: false,
            },
        };
    }
    case RESET_PASSWORD_SUCCESS: {
        return {
            ...state,
            passwordReset: {
                error: '',
                reset: true,
            },
        };
    }
    default:
        return state;
    }
}
