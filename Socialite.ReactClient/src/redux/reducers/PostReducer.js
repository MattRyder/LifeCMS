import {
    FETCH_USER_POSTS_BEGIN,
    FETCH_USER_POSTS_SUCCESS,
    FETCH_USER_POSTS_FAILURE
} from '../actions/PostActions';
import Post, { PostStates } from '../../components/Posts/Post';

const InitialState = {
    posts: [],
    loading: false,
    error: null,
};

const createPost = json => {
    return new Post(json.title, PostStates.PUBLISHED, json.text, new Date(json.created_at));
};

const PostReducer = (state = InitialState, action) => {
    switch(action.type) {
        case FETCH_USER_POSTS_BEGIN:
            return {
                ...state,
                loading: true,
                errors: null,
            };
        case FETCH_USER_POSTS_SUCCESS:
            return {
                ...state,
                loading: false,
                posts: action.payload.posts.map(p => createPost(p)),
            };
        case FETCH_USER_POSTS_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.payload.error,
            };
        default:
            return state;
    }
};

export default PostReducer;