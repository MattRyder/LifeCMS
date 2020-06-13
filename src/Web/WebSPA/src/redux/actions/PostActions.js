import { getLifeCMSApi } from '../LifeCMSApi';

export const FETCH_USER_POSTS_BEGIN = 'FETCH_USER_POSTS_BEGIN';
export const FETCH_USER_POSTS_SUCCESS = 'FETCH_USER_POSTS_SUCCESS';
export const FETCH_USER_POSTS_FAILURE = 'FETCH_USER_POSTS_FAILURE';

export const fetchUserPostsBegin = (userId) => ({
    type: FETCH_USER_POSTS_BEGIN,
    payload: { userId },
});

export const fetchUserPostsSuccess = (userId, posts) => ({
    type: FETCH_USER_POSTS_SUCCESS,
    payload: { userId, posts },
});

export const fetchUserPostsFailure = (userId, error) => ({
    type: FETCH_USER_POSTS_FAILURE,
    payload: { userId, error },
});

export const fetchPosts = (accessToken, userId) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    dispatch(fetchUserPostsBegin(userId));

    try {
        const response = await lifecmsApi.getPosts(userId);

        const { data } = response;

        dispatch(fetchUserPostsSuccess(userId, data));
    } catch (error) {
        dispatch(fetchUserPostsFailure(userId, error));
    }
};

export const createPost = (accessToken, postParams) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    return lifecmsApi.createPost(postParams);
};
