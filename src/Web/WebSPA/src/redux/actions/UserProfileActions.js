import { getLifeCMSApi } from '../LifeCMSApi';

export const FETCH_USER_PROFILE_BEGIN = 'FETCH_USER_PROFILE_BEGIN';
export const FETCH_USER_PROFILE_SUCCESS = 'FETCH_USER_PROFILE_SUCCESS';
export const FETCH_USER_PROFILE_FAILURE = 'FETCH_USER_PROFILE_FAILURE';

export const fetchUserProfileBegin = (userId) => ({
    type: FETCH_USER_PROFILE_BEGIN,
    payload: { userId },
});

export const fetchUserProfileSuccess = (userId, userProfile) => ({
    type: FETCH_USER_PROFILE_SUCCESS,
    payload: { userId, userProfile },
});

export const fetchUserProfileFailure = (userId, error) => ({
    type: FETCH_USER_PROFILE_FAILURE,
    payload: { userId, error },
});

export const fetchUserProfile = (accessToken, userId) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    dispatch(fetchUserProfileBegin(userId));

    try {
        const response = await lifecmsApi.getUserProfile(userId);

        const { data } = response;

        dispatch(fetchUserProfileSuccess(userId, data));
    } catch (error) {
        dispatch(fetchUserProfileFailure(userId, error));
    }
};

export const createUserProfile = (accessToken, userProfileParams) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    return lifecmsApi.createUserProfile(userProfileParams);
};
