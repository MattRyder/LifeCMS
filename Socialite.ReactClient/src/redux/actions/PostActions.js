import SocialiteApi from '../SocialiteApi';

export const FETCH_USER_POSTS_BEGIN = "FETCH_USER_POSTS_BEGIN";
export const FETCH_USER_POSTS_SUCCESS = "FETCH_USER_POSTS_SUCCESS";
export const FETCH_USER_POSTS_FAILURE = "FETCH_USER_POSTS_FAILURE";

export const fetchUserPostsBegin = () => ({
    type: FETCH_USER_POSTS_BEGIN,
});

export const fetchUserPostsSuccess = (posts) => ({
    type: FETCH_USER_POSTS_SUCCESS,
    payload: { posts },
});

export const fetchUserPostsFailure = (error) => ({
    type: FETCH_USER_POSTS_FAILURE,
    payload: { error }
});

export const fetchPosts = () => {
    var socialiteApi = new SocialiteApi(process.env.REACT_APP_API_HOST, process.env.REACT_APP_API_KEY)

    return dispatch => {
        dispatch(fetchUserPostsBegin());

        return socialiteApi
            .getPosts()
            .catch(error => {
                dispatch(fetchUserPostsFailure(error));
            })
            .then(response => {
                var posts = response.data;

                dispatch(fetchUserPostsSuccess(posts));

                return posts;
            });
    };
}