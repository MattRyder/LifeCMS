import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import { reducer as oidcReducer } from 'redux-oidc';
import { toastsReducer } from 'react-toastify-redux';
import {
    CampaignReducer,
    PostReducer,
    UserProfileReducer,
    NewsletterReducer,
} from './reducers';

export default (history) => combineReducers({
    router: connectRouter(history),
    post: PostReducer,
    newsletter: NewsletterReducer,
    userProfile: UserProfileReducer,
    campaign: CampaignReducer,
    oidc: oidcReducer,
    toasts: toastsReducer,
});
