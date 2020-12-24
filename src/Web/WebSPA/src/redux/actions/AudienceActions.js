import { createAction } from '@reduxjs/toolkit';
import {
    success as toastSuccess,
    error as toastError,
} from 'react-toastify-redux';
import { push } from 'connected-react-router';
import { getLifeCMSApi } from 'redux/LifeCMSApi';

export const FETCH_AUDIENCE = 'audience/FETCH_AUDIENCE';
export const CREATE_AUDIENCE = 'audience/CREATE_AUDIENCE';
export const DELETE_AUDIENCE = 'audience/DELETE_AUDIENCE';
export const UPDATE_AUDIENCE_NAME = 'audience/DELETE_AUDIENCE';

export const fetchAudienceAction = createAction(FETCH_AUDIENCE);
export const createAudienceAction = createAction(CREATE_AUDIENCE);
export const deleteAudienceAction = createAction(DELETE_AUDIENCE);
export const updateAudienceNameAction = createAction(UPDATE_AUDIENCE_NAME);

export const fetchAudiences = (accessToken) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        const response = await lifecmsApi.getAudiences();

        response.data.map((audience) => dispatch(fetchAudienceAction(audience)));
    } catch (error) {
        console.error(error);
    }
};

const createAudience = async (accessToken, params) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    const { data } = await lifecmsApi.createAudience(params);

    if (!data.success) {
        throw new Error(data.error);
    }

    return data.data;
};

export const createAudienceAndRedirect = (accessToken, params) => async (dispatch) => {
    const { id } = await createAudience(accessToken, params);

    dispatch(push(`/audiences/${id}/add-subscribers`));

    dispatch(toastSuccess('Sucessfully created an audience.'));
};

export const deleteAudience = (accessToken, audienceId) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        await lifecmsApi.deleteAudience(audienceId);

        dispatch(deleteAudienceAction(audienceId));

        dispatch(toastSuccess('Successfully deleted the Audience.'));
    } catch (error) {
        console.error(error);

        dispatch(toastError(error.message));
    }
};

export const updateAudienceName = ({
    accessToken, audienceId, name, redirectTo,
}) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        await lifecmsApi.updateAudienceName(audienceId, { name });

        dispatch(updateAudienceNameAction(audienceId, name));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        console.error(error);
    }
};
