import {
    FETCH_USER_POSTS_BEGIN,
    FETCH_USER_POSTS_SUCCESS,
    FETCH_USER_POSTS_FAILURE,
} from '../actions/PostActions';

const PostReducer = (state = {}, action) => {
    switch (action.type) {
    case FETCH_USER_POSTS_BEGIN:
        return {
            ...state,
            [action.payload.userId]: {
                posts: [],
                loading: true,
                error: null,
            },
        };
    case FETCH_USER_POSTS_SUCCESS:
        return {
            ...state,
            [action.payload.userId]: {
                posts: action.payload.posts,
                loading: false,
            },
        };
    case FETCH_USER_POSTS_FAILURE:
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

export default PostReducer;
