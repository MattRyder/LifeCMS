import {
    FETCH_USER_PROFILE_BEGIN,
    FETCH_USER_PROFILE_SUCCESS,
    FETCH_USER_PROFILE_FAILURE,
} from '../actions/UserProfileActions';

export default (state = {}, action) => {
    switch (action.type) {
    case FETCH_USER_PROFILE_BEGIN:
        return {
            ...state,
            [action.payload.userId]: {
                userProfile: [],
                loading: true,
                error: null,
            },
        };
    case FETCH_USER_PROFILE_SUCCESS:
        return {
            ...state,
            [action.payload.userId]: {
                userProfile: action.payload.userProfile,
                loading: false,
            },
        };
    case FETCH_USER_PROFILE_FAILURE:
        return {
            ...state,
            [action.payload.userId]: {
                loading: true,
                error: action.payload.error,
            },
        };
    default:
        return state;
    }
};
