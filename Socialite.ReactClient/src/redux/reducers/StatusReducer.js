import {
    FETCH_USER_STATUSES_BEGIN,
    FETCH_USER_STATUSES_FAILURE,
    FETCH_USER_STATUSES_SUCCESS,
} from '../actions/StatusActions';

const StatusReducer = (state = {}, action) => {
    switch (action.type) {
    case FETCH_USER_STATUSES_BEGIN:
        return {
            ...state,
            [action.payload.userId]: {
                loading: true,
                errors: null,
            },
        };
    case FETCH_USER_STATUSES_SUCCESS:
        return {
            ...state,
            [action.payload.userId]: {
                loading: false,
                statuses: action.payload.statuses,
            },
        };
    case FETCH_USER_STATUSES_FAILURE:
        return {
            ...state,
            [action.payload.userId]: {
                loading: false,
                error: action.payload.error,
            },
        };
    default:
        return state;
    }
};

export default StatusReducer;