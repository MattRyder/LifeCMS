import { createAction } from '@reduxjs/toolkit';
import { push } from 'connected-react-router';
import { success as toastSuccess, error as toastError } from 'react-toastify-redux';
import { getLifeCMSApi } from '../LifeCMSApi';

export const FETCH_USER_PROFILE = 'userProfile/FETCH_USER_PROFILE';
export const DELETE_USER_PROFILE = 'userProfile/DELETE_USER_PROFILE';

export const fetchUserProfile = createAction(FETCH_USER_PROFILE);
export const deleteUserProfile = createAction(DELETE_USER_PROFILE);

export const CREATE_USER_PROFILE_SUCCESS = 'CREATE_USER_PROFILE_SUCCESS';
export const CREATE_USER_PROFILE_FAILURE = 'CREATE_USER_PROFILE_FAILURE';

export const createUserProfileSuccess = (userId, userProfile) => ({
    type: CREATE_USER_PROFILE_SUCCESS,
    payload: { userId, userProfile },
});

export const createUserProfileFailure = (userId, error) => ({
    type: CREATE_USER_PROFILE_FAILURE,
    payload: { userId, error },
});

export const fetchUserProfiles = (accessToken, userId) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        const response = await lifecmsApi.getUserProfiles(userId);

        response.data.map((profile) => dispatch(fetchUserProfile(profile)));
    } catch (error) {
        console.error(error);
    }
};

export const createUserProfile = (
    accessToken, userId, userProfileParams, redirectTo,
) => async (dispatch) => {
    try {
        const response = await getLifeCMSApi(accessToken)
            .createUserProfile(userId, userProfileParams);

        const { data: userProfile } = response;

        dispatch(createUserProfileSuccess(userId, userProfile));

        dispatch(toastSuccess('Successfully created the User Profile.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        dispatch(createUserProfileFailure(userId, error));

        dispatch(toastError(error.message));
    }
};

export const performUserProfileDelete = (
    accessToken, userId, userProfileId,
) => async (dispatch) => {
    try {
        await getLifeCMSApi(accessToken)
            .deleteUserProfile(userId, userProfileId);

        dispatch(deleteUserProfile(userProfileId));

        dispatch(toastSuccess('Successfully deleted the user profile.'));
    } catch (error) {
        dispatch(toastError(error.message));
    }
};
