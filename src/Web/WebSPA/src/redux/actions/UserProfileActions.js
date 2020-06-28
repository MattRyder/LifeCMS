import { success as toastSuccess, error as toastError } from 'react-toastify-redux';
import { getLifeCMSApi } from '../LifeCMSApi';

export const FETCH_USER_PROFILES_BEGIN = 'FETCH_USER_PROFILES_BEGIN';
export const FETCH_USER_PROFILES_SUCCESS = 'FETCH_USER_PROFILES_SUCCESS';
export const FETCH_USER_PROFILES_FAILURE = 'FETCH_USER_PROFILES_FAILURE';

export const CREATE_USER_PROFILE_SUCCESS = 'CREATE_USER_PROFILE_SUCCESS';
export const CREATE_USER_PROFILE_FAILURE = 'CREATE_USER_PROFILE_FAILURE';

export const DELETE_USER_PROFILE_SUCCESS = 'DELETE_USER_PROFILE_SUCCESS';
export const DELETE_USER_PROFILE_FAILURE = 'DELETE_USER_PROFILE_FAILURE';

export const fetchUserProfilesBegin = (userId) => ({
    type: FETCH_USER_PROFILES_BEGIN,
    payload: { userId },
});

export const fetchUserProfilesSuccess = (userId, userProfiles) => ({
    type: FETCH_USER_PROFILES_SUCCESS,
    payload: { userId, userProfiles },
});

export const fetchUserProfilesFailure = (userId, error) => ({
    type: FETCH_USER_PROFILES_FAILURE,
    payload: { userId, error },
});

export const createUserProfileSuccess = (userId, userProfile) => ({
    type: CREATE_USER_PROFILE_SUCCESS,
    payload: { userId, userProfile },
});

export const createUserProfileFailure = (userId, error) => ({
    type: CREATE_USER_PROFILE_FAILURE,
    payload: { userId, error },
});

export const deleteUserProfileSuccess = (userId, userProfileId) => ({
    type: DELETE_USER_PROFILE_SUCCESS,
    payload: { userId, userProfileId },
});

export const deleteUserProfileFailure = (userId, userProfileId) => ({
    type: DELETE_USER_PROFILE_SUCCESS,
    payload: { userId, userProfileId },
});

export const fetchUserProfiles = (accessToken, userId) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    dispatch(fetchUserProfilesBegin(userId));

    try {
        const response = await lifecmsApi.getUserProfiles(userId);

        const { data } = response;

        dispatch(fetchUserProfilesSuccess(userId, data));
    } catch (error) {
        dispatch(fetchUserProfilesFailure(userId, error));
    }
};

export const createUserProfile = (accessToken, userId, userProfileParams) => async (dispatch) => {
    try {
        const response = await getLifeCMSApi(accessToken)
            .createUserProfile(userId, userProfileParams);

        const { data: userProfile } = response;

        dispatch(createUserProfileSuccess(userId, userProfile));

        dispatch(toastSuccess('Successfully created an Identity.'));
    } catch (error) {
        dispatch(createUserProfileFailure(userId, error));

        dispatch(toastError(error.message));
    }
};

export const deleteUserProfile = (accessToken, userId, userProfileId) => async (dispatch) => {
    try {
        await getLifeCMSApi(accessToken)
            .deleteUserProfile(userId, userProfileId);

        dispatch(deleteUserProfileSuccess(userId, userProfileId));

        dispatch(toastSuccess('Successfully deleted the Identity.'));
    } catch (error) {
        dispatch(deleteUserProfileFailure(userId, userProfileId));

        dispatch(toastError(error.message));
    }
};
