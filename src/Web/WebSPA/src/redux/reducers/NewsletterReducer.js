import {
    FETCH_NEWSLETTERS_BEGIN,
    FETCH_NEWSLETTERS_FAILURE,
    FETCH_NEWSLETTERS_SUCCESS,
    DELETE_NEWSLETTER_SUCCESS,
} from '../actions/NewsletterActions';

const NewsletterReducer = (state = {}, action) => {
    switch (action.type) {
    case FETCH_NEWSLETTERS_BEGIN:
        return {
            ...state,
            [action.payload.userId]: {
                newsletters: [],
                loading: true,
                error: null,
            },
        };
    case FETCH_NEWSLETTERS_SUCCESS:
        return {
            ...state,
            [action.payload.userId]: {
                newsletters: action.payload.newsletters,
                loading: false,
            },
        };
    case FETCH_NEWSLETTERS_FAILURE:
        return {
            ...state,
            [action.payload.userId]: {
                loading: false,
                error: action.payload.error,
            },
        };
    case DELETE_NEWSLETTER_SUCCESS: {
        const { userId, newsletterId } = action.payload;
        return {
            ...state,
            [userId]: {
                ...state[userId],
                newsletters: state[userId]
                    .newsletters
                    .filter((newsletter) => newsletter.id !== newsletterId),
            },
        };
    }
    default:
        return state;
    }
};

export default NewsletterReducer;
