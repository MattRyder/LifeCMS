import { getSocialiteApi } from '../SocialiteApi';

export const FETCH_USER_STATUSES_BEGIN = 'FETCH_USER_STATUSES_BEGIN';
export const FETCH_USER_STATUSES_SUCCESS = 'FETCH_USER_STATUSES_SUCCESS';
export const FETCH_USER_STATUSES_FAILURE = 'FETCH_USER_STATUSES_FAILURE';

export const fetchUserStatusesBegin = (userId) => ({
    type: FETCH_USER_STATUSES_BEGIN,
    payload: { userId },
});

export const fetchUserStatusesSuccess = (userId, statuses) => ({
    type: FETCH_USER_STATUSES_SUCCESS,
    payload: { userId, statuses },
});

export const fetchUserStatusesFailure = (userId, error) => ({
    type: FETCH_USER_STATUSES_FAILURE,
    payload: { userId, error },
});

export const fetchStatuses = (accessToken, userId) => async (dispatch) => {
    const socialiteApi = getSocialiteApi(accessToken);

    dispatch(fetchUserStatusesBegin(userId));

    try {
        const response = await socialiteApi.getStatuses();

        const { data } = response;

        dispatch(fetchUserStatusesSuccess(userId, data));
    } catch (error) {
        dispatch(fetchUserStatusesFailure(userId, error.message));
    }
};

export const createStatus = async (accessToken, statusParams) => {
    const socialiteApi = getSocialiteApi(accessToken);

    await socialiteApi.createStatus(statusParams);
};
