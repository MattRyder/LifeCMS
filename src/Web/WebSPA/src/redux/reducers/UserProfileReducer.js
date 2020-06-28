import {
    FETCH_USER_PROFILES_BEGIN,
    FETCH_USER_PROFILES_SUCCESS,
    FETCH_USER_PROFILES_FAILURE,
    DELETE_USER_PROFILE_SUCCESS,
    CREATE_USER_PROFILE_SUCCESS,
} from '../actions/UserProfileActions';

export default (state = {}, action) => {
    switch (action.type) {
    case FETCH_USER_PROFILES_BEGIN:
        return {
            ...state,
            [action.payload.userId]: {
                userProfile: [],
                loading: true,
                error: null,
            },
        };
    case FETCH_USER_PROFILES_SUCCESS:
        return {
            ...state,
            [action.payload.userId]: {
                userProfiles: action.payload.userProfiles,
                loading: false,
            },
        };
    case FETCH_USER_PROFILES_FAILURE:
        return {
            ...state,
            [action.payload.userId]: {
                loading: true,
                error: action.payload.error,
            },
        };
    case CREATE_USER_PROFILE_SUCCESS:
        return {
            ...state,
            [action.payload.userId]: {
                ...state[action.payload.userId],
                userProfiles: state[action.payload.userId]
                    .userProfiles
                    .concat(action.payload.userProfile),
            },
        };
    case DELETE_USER_PROFILE_SUCCESS:
        return {
            ...state,
            [action.payload.userId]: {
                ...state[action.payload.userId],
                userProfiles: state[action.payload.userId]
                    .userProfiles
                    .filter((up) => up.id !== action.payload.userProfileId),
            },
        };
    default:
        return state;
    }
};
