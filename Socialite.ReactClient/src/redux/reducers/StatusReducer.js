import {
    FETCH_USER_STATUSES_BEGIN,
    FETCH_USER_STATUSES_FAILURE,
    FETCH_USER_STATUSES_SUCCESS,
} from '../actions/StatusActions';
import Status from '../../components/Statuses/Status';

const InitialState = {
    statuses: [],
    loading: false,
    error: null,
};

const StatusReducer = (state = InitialState, action) => {
    switch(action.type) {
        case FETCH_USER_STATUSES_BEGIN:
            return {
                ...state,
                loading: true,
                errors: null,
            };
        case FETCH_USER_STATUSES_SUCCESS:
            return {
                ...state,
                loading: false,
                statuses: action.payload.statuses.map(s => new Status(s.mood, s.text, new Date(s.created_at))),
            };
        case FETCH_USER_STATUSES_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.payload.error,
            };
        default:
            return state;
    }
};

export default StatusReducer;