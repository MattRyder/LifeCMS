import {
    RAISE_ERROR,
} from '../actions/ErrorActions';

const InitialState = {
    error: '',
};

export default function ErrorReducer(state = InitialState, action) {
    switch (action.type) {
    case RAISE_ERROR: {
        return {
            ...state,
            error: action.payload.error,
        };
    }
    default:
        return state;
    }
}
