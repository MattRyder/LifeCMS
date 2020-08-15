import { success as toastSuccess, error as toastError } from 'react-toastify-redux';
import { push } from 'connected-react-router';
import { getLifeCMSApi } from '../LifeCMSApi';

export const CREATE_NEWSLETTER_BEGIN = 'CREATE_NEWSLETTER_BEGIN';
export const CREATE_NEWSLETTER_SUCCESS = 'CREATE_NEWSLETTER_SUCCESS';
export const CREATE_NEWSLETTER_FAILURE = 'CREATE_NEWSLETTER_FAILURE';

export const EDIT_NEWSLETTER_SUCCESS = 'EDIT_NEWSLETTER_SUCCESS';

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

export const editNewsletterBodySuccess = (userId, newsletterId) => ({
    type: EDIT_NEWSLETTER_SUCCESS,
    payload: { userId, newsletterId },
});

export const editNewsletterBodyFailure = (userId, newsletterId) => ({
    type: EDIT_NEWSLETTER_SUCCESS,
    payload: { userId, newsletterId },
});

export const fetchNewslettersBegin = (userId) => ({
    type: FETCH_NEWSLETTERS_BEGIN,
    payload: { userId },
});

export const fetchNewslettersSuccess = (userId, newsletters) => ({
    type: FETCH_NEWSLETTERS_SUCCESS,
    payload: { userId, newsletters },
});

export const fetchNewslettersFailure = (userId, error) => ({
    type: FETCH_NEWSLETTERS_FAILURE,
    payload: { userId, error },
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
        dispatch(fetchNewslettersFailure(userId, error.message));

        dispatch(toastError(error.message));
    }
};

export const createNewsletter = (
    accessToken, userId, params, redirectTo,
) => async (dispatch) => {
    dispatch(createNewsletterBegin());

    try {
        const response = await getLifeCMSApi(accessToken)
            .createNewsletter(userId, params);

        const { data } = response;

        dispatch(createNewsletterSuccess(userId, data));

        dispatch(toastSuccess('Successfully created the newsletter.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        dispatch(createNewsletterFailure(userId, error.message));

        dispatch(toastError(error.message));
    }
};

export const editNewsletterBody = (
    accessToken, userId, newsletterId, designSource, redirectTo,
) => async (dispatch) => {
    try {
        await getLifeCMSApi(accessToken)
            .editNewsletterBody(userId, newsletterId, designSource);

        dispatch(editNewsletterBodySuccess(userId, newsletterId));

        dispatch(toastSuccess('Successfully edited the newsletter\'s design.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        dispatch(editNewsletterBodyFailure(userId, error.message));

        dispatch(toastError(error.message));
    }
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
        dispatch(deleteNewsletterFailure(userId, error.message));

        dispatch(toastError(error.message));
    }
};
