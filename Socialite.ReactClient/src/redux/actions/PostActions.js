import SocialiteApi from '../SocialiteApi';

export const FETCH_USER_POSTS_BEGIN = 'FETCH_USER_POSTS_BEGIN';
export const FETCH_USER_POSTS_SUCCESS = 'FETCH_USER_POSTS_SUCCESS';
export const FETCH_USER_POSTS_FAILURE = 'FETCH_USER_POSTS_FAILURE';

export const fetchUserPostsBegin = () => ({
    type: FETCH_USER_POSTS_BEGIN,
});

export const fetchUserPostsSuccess = (posts) => ({
    type: FETCH_USER_POSTS_SUCCESS,
    payload: { posts },
});

export const fetchUserPostsFailure = (error) => ({
    type: FETCH_USER_POSTS_FAILURE,
    payload: { error },
});

export const fetchPosts = () => async (dispatch) => {
    const socialiteApi = new SocialiteApi(
        process.env.REACT_APP_API_HOST,
        process.env.REACT_APP_API_KEY,
    );

    dispatch(fetchUserPostsBegin());

    try {
        const response = await socialiteApi.getPosts();

        const { data } = response;

        dispatch(fetchUserPostsSuccess(data));
    } catch (error) {
        dispatch(fetchUserPostsFailure(error));
    }
};
