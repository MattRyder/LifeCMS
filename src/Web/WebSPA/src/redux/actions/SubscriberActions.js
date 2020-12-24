import { createAction } from '@reduxjs/toolkit';
import { getLifeCMSApi } from '../LifeCMSApi';

export const FETCH_SUBSCRIBER = 'FETCH_SUBSCRIBER';

export const fetchSubscriberAction = createAction(FETCH_SUBSCRIBER);

export const addSubscriber = (accessToken, params) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        await lifecmsApi.addSubscriber(params);
    } catch (error) {
        console.error(error);
    }
};

export const confirmSubscriber = async (audienceId, subscriberToken) => {
    const lifecmsApi = getLifeCMSApi();

    return lifecmsApi.confirmSubscriber({
        audience_id: audienceId,
        subscriber_token: subscriberToken,
    });
};

export const fetchSubscribers = (accessToken, audienceId) => async (dispatch) => {
    const lifecmsApi = getLifeCMSApi(accessToken);

    try {
        const response = await lifecmsApi.getAudienceSubscribers(audienceId);

        response.data.map((subscriber) => dispatch(fetchSubscriberAction(subscriber)));
    } catch (error) {
        console.error(error);
    }
};
