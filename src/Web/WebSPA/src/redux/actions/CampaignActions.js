import {
    success as toastSuccess,
    error as toastError,
} from 'react-toastify-redux';
import { createAction } from '@reduxjs/toolkit';
import { push } from 'connected-react-router';
import { getLifeCMSApi } from '../LifeCMSApi';

// export const CREATE_CAMPAIGN = 'campaign/CREATE';
export const FETCH_CAMPAIGN = 'campaign/FETCH';
export const DELETE_CAMPAIGN = 'campaign/DELETE';

// export const createCampaignAction = createAction(CREATE_CAMPAIGN);
export const fetchCampaignAction = createAction(FETCH_CAMPAIGN);
export const deleteCampaignAction = createAction(DELETE_CAMPAIGN);

export const fetchCampaigns = (accessToken, userId) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        const response = await lifecmsApi.getCampaigns(userId);

        response.data.map((campaign) => dispatch(fetchCampaignAction(campaign)));
    } catch (error) {
        console.error(error);
    }
};

export const createCampaign = (
    accessToken, params, redirectTo,
) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        await lifecmsApi.createCampaign(params);

        dispatch(toastSuccess('Successfully created a new Campaign.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        toastError(error.message);
    }
};

export const updateCampaignSubject = (
    accessToken, campaignId, params, redirectTo,
) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        await lifecmsApi.updateCampaignSubject(campaignId, params);

        dispatch(toastSuccess('Successfully updated the Campaign\'s subject.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        toastError(error.message);
    }
};

export const updateCampaignName = (
    accessToken, campaignId, params, redirectTo,
) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        await lifecmsApi.updateCampaignName(campaignId, params);

        dispatch(toastSuccess('Successfully updated the Campaign\'s name.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        toastError(error.message);
    }
};

export const deleteCampaign = (
    accessToken, campaignId, redirectTo,
) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        await lifecmsApi.deleteCampaign(campaignId);

        dispatch(deleteCampaignAction(campaignId));

        dispatch(toastSuccess('Successfully deleted the Campaign.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        toastError(error.message);
    }
};
