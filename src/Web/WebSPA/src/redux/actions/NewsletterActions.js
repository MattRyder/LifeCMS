import { success as toastSuccess, error as toastError } from 'react-toastify-redux';
import { getLifeCMSApi } from '../LifeCMSApi';

export const CREATE_NEWSLETTER_BEGIN = 'CREATE_NEWSLETTER_BEGIN';
export const CREATE_NEWSLETTER_SUCCESS = 'CREATE_NEWSLETTER_SUCCESS';
export const CREATE_NEWSLETTER_FAILURE = 'CREATE_NEWSLETTER_FAILURE';

export const FETCH_NEWSLETTERS_BEGIN = 'FETCH_NEWSLETTERS_BEGIN';
export const FETCH_NEWSLETTERS_SUCCESS = 'FETCH_NEWSLETTERS_SUCCESS';
export const FETCH_NEWSLETTERS_FAILURE = 'FETCH_NEWSLETTERS_FAILURE';

export const DELETE_NEWSLETTER_SUCCESS = 'DELETE_NEWSLETTER_SUCCESS';
export const DELETE_NEWSLETTER_FAILURE = 'DELETE_NEWSLETTER_FAILURE';

export const createNewsletterBegin = () => ({
    type: CREATE_NEWSLETTER_BEGIN,
});

export const createNewsletterSuccess = () => ({
    type: CREATE_NEWSLETTER_SUCCESS,
});

export const createNewsletterFailure = () => ({
    type: CREATE_NEWSLETTER_FAILURE,
});

export const fetchNewslettersBegin = (userId) => ({
    type: FETCH_NEWSLETTERS_BEGIN,
    payload: { userId },
});

export const fetchNewslettersSuccess = (userId, newsletters) => ({
    type: FETCH_NEWSLETTERS_SUCCESS,
    payload: { userId, newsletters },

});

export const fetchNewslettersFailure = () => ({
    type: FETCH_NEWSLETTERS_FAILURE,
});

export const deleteNewsletterSuccess = (userId, newsletterId) => ({
    type: 'DELETE_NEWSLETTER_SUCCESS',
    payload: { userId, newsletterId },
});

export const deleteNewsletterFailure = (userId, newsletterId) => ({
    type: 'DELETE_NEWSLETTER_FAILURE',
    payload: { userId, newsletterId },
});

export const fetchNewsletters = (accessToken, userId) => async (dispatch) => {
    dispatch(fetchNewslettersBegin(userId));

    try {
        const response = await getLifeCMSApi(accessToken)
            .getNewsletters(userId);

        dispatch(fetchNewslettersSuccess(userId, response.data));
    } catch (error) {
        dispatch(fetchNewslettersFailure(userId, error));
    }
};

export const createNewsletter = (
    accessToken, userId, params,
) => async (dispatch) => {
    dispatch(createNewsletterBegin());

    try {
        const response = await getLifeCMSApi(accessToken)
            .createNewsletter(userId, params);

        const { data } = response;

        dispatch(createNewsletterSuccess(userId, data));
    } catch (error) {
        dispatch(createNewsletterFailure(userId, error));
    }
};

export const editNewsletter = (
    accessToken, userId, newsletterId, designSource,
) => async (dispatch) => {
    throw new Error('Not Implemented.');
};

export const deleteNewsletter = (
    accessToken, userId, newsletterId,
) => async (dispatch) => {
    dispatch(createNewsletterBegin());

    try {
        await getLifeCMSApi(accessToken)
            .deleteNewsletter(userId, newsletterId);

        dispatch(deleteNewsletterSuccess(userId, newsletterId));

        dispatch(toastSuccess('Successfully deleted the newsletter.'));
    } catch (error) {
        dispatch(deleteNewsletterFailure(userId, newsletterId));

        dispatch(toastError(error.message));
    }
};
