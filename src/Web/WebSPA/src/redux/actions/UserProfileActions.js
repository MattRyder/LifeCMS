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

export const createUserProfileSuccess = (userProfile) => ({
    type: CREATE_USER_PROFILE_SUCCESS,
    payload: { userProfile },
});

export const createUserProfileFailure = (userId, error) => ({
    type: CREATE_USER_PROFILE_FAILURE,
    payload: { userId, error },
});

export const fetchUserProfiles = (accessToken) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        const response = await lifecmsApi.getUserProfiles();

        response.data.map((profile) => dispatch(fetchUserProfile(profile)));
    } catch (error) {
        console.error(error);
    }
};

export const createUserProfile = (
    accessToken, userProfileParams, redirectTo,
) => async (dispatch) => {
    try {
        const response = await getLifeCMSApi(accessToken)
            .createUserProfile(userProfileParams);

        const { data: userProfile } = response;

        dispatch(createUserProfileSuccess(userProfile));

        dispatch(toastSuccess('Successfully created the User Profile.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        dispatch(createUserProfileFailure(error));

        dispatch(toastError(error.message));
    }
};

export const editUserProfile = ({
    accessToken, params, redirectTo,
}) => async (dispatch) => {
    try {
        await getLifeCMSApi(accessToken)
            .editUserProfile(params);

        dispatch(toastSuccess('Successfully updated the User Profile.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        dispatch(toastError(error.message));
    }
};

export const performUserProfileDelete = (
    accessToken, userProfileId,
) => async (dispatch) => {
    try {
        await getLifeCMSApi(accessToken).deleteUserProfile(userProfileId);

        dispatch(deleteUserProfile(userProfileId));

        dispatch(toastSuccess('Successfully deleted the user profile.'));
    } catch (error) {
        dispatch(toastError(error.message));
    }
};
