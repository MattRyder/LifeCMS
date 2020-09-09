import {
    success as toastSuccess,
    error as toastError,
} from 'react-toastify-redux';
import { push } from 'connected-react-router';
import { getLifeCMSApi } from '../LifeCMSApi';

export const FETCH_USER_CAMPAIGNS_BEGIN = 'FETCH_USER_CAMPAIGNS_BEGIN';
export const FETCH_USER_CAMPAIGNS_SUCCESS = 'FETCH_USER_CAMPAIGNS_SUCCESS';
export const FETCH_USER_CAMPAIGNS_FAILURE = 'FETCH_USER_CAMPAIGNS_FAILURE';
export const CREATE_USER_CAMPAIGN_SUCCESS = 'CREATE_USER_CAMPAIGN_SUCCESS';
export const DELETE_USER_CAMPAIGN_SUCCESS = 'DELETE_USER_CAMPAIGN_SUCCESS';

export const fetchUserCampaignsBegin = (userId) => ({
    type: FETCH_USER_CAMPAIGNS_BEGIN,
    payload: { userId },
});

export const fetchUserCampaignsSuccess = (userId, campaigns) => ({
    type: FETCH_USER_CAMPAIGNS_SUCCESS,
    payload: { userId, campaigns },
});

export const fetchUserCampaignsFailure = (userId, error) => ({
    type: FETCH_USER_CAMPAIGNS_FAILURE,
    payload: { userId, error },
});

export const createCampaignSuccess = (userId) => ({
    type: CREATE_USER_CAMPAIGN_SUCCESS,
    payload: { userId },
});

export const deleteCampaignSuccess = (userId, campaignId) => ({
    type: DELETE_USER_CAMPAIGN_SUCCESS,
    payload: { userId, campaignId },
});

export const fetchCampaigns = (accessToken, userId) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    dispatch(fetchUserCampaignsBegin(userId));

    try {
        const response = await lifecmsApi.getCampaigns(userId);

        const { data } = response;

        dispatch(fetchUserCampaignsSuccess(userId, data));
    } catch (error) {
        dispatch(fetchUserCampaignsFailure(userId, error));
    }
};

export const createCampaign = (
    accessToken, userId, params, redirectTo,
) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        await lifecmsApi.createCampaign(params);

        dispatch(createCampaignSuccess(userId));

        dispatch(toastSuccess('Successfully created a new Campaign.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        toastError(error.message);
    }
};

export const updateCampaignSubject = (
    accessToken, userId, campaignId, params, redirectTo,
) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        await lifecmsApi.updateCampaignSubject(campaignId, params);

        dispatch(createCampaignSuccess(userId));

        dispatch(toastSuccess('Successfully updated the Campaign\'s subject.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        toastError(error.message);
    }
};

export const updateCampaignName = (
    accessToken, userId, campaignId, params, redirectTo,
) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        await lifecmsApi.updateCampaignName(campaignId, params);

        dispatch(createCampaignSuccess(userId));

        dispatch(toastSuccess('Successfully updated the Campaign\'s name.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        toastError(error.message);
    }
};

export const deleteCampaign = (
    accessToken, userId, campaignId, redirectTo,
) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        await lifecmsApi.deleteCampaign(campaignId);

        dispatch(deleteCampaignSuccess(userId, campaignId));

        dispatch(toastSuccess('Successfully deleted the Campaign.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        toastError(error.message);
    }
};
