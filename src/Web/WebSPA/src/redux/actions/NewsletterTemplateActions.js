import { success as toastSuccess, error as toastError } from 'react-toastify-redux';
import { push } from 'connected-react-router';
import { createAction } from '@reduxjs/toolkit';
import { getLifeCMSApi } from '../LifeCMSApi';

export const FETCH_NEWSLETTER_TEMPLATE = 'newsletterTemplate/FETCH_NEWSLETTER_TEMPLATE';
export const DELETE_NEWSLETTER_TEMPLATE = 'newsletterTemplate/DELETE_NEWSLETTER_TEMPLATE';

export const fetchNewsletterTemplateAction = createAction(FETCH_NEWSLETTER_TEMPLATE);
export const deleteNewsletterTemplateAction = createAction(DELETE_NEWSLETTER_TEMPLATE);

export const CREATE_NEWSLETTER_BEGIN = 'CREATE_NEWSLETTER_BEGIN';
export const CREATE_NEWSLETTER_SUCCESS = 'CREATE_NEWSLETTER_SUCCESS';
export const CREATE_NEWSLETTER_FAILURE = 'CREATE_NEWSLETTER_FAILURE';

export const EDIT_NEWSLETTER_SUCCESS = 'EDIT_NEWSLETTER_SUCCESS';

export const createNewsletterTemplateBegin = () => ({
    type: CREATE_NEWSLETTER_BEGIN,
});

export const createNewsletterTemplateSuccess = () => ({
    type: CREATE_NEWSLETTER_SUCCESS,
});

export const createNewsletterTemplateFailure = () => ({
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

export const fetchNewsletters = (accessToken, userId) => async (dispatch) => {
    try {
        const response = await getLifeCMSApi(accessToken)
            .getNewsletters(userId);

        response.data.map((newsletterTemplate) => dispatch(
            fetchNewsletterTemplateAction(newsletterTemplate),
        ));
    } catch (error) {
        dispatch(toastError(error.message));
    }
};

export const createNewsletterTemplate = (
    accessToken, userId, params, redirectTo,
) => async (dispatch) => {
    dispatch(createNewsletterTemplateBegin());

    try {
        const response = await getLifeCMSApi(accessToken)
            .createNewsletter(userId, params);

        const { data } = response;

        dispatch(createNewsletterTemplateSuccess(userId, data));

        dispatch(toastSuccess('Successfully created the newsletter design.'));

        if (redirectTo) {
            dispatch(push(redirectTo));
        }
    } catch (error) {
        dispatch(createNewsletterTemplateFailure(userId, error.message));

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
    try {
        await getLifeCMSApi(accessToken)
            .deleteNewsletter(userId, newsletterId);

        dispatch(deleteNewsletterTemplateAction(newsletterId));

        dispatch(toastSuccess('Successfully deleted the newsletter.'));
    } catch (error) {
        dispatch(toastError(error.message));
    }
};
