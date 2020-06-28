import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import { reducer as oidcReducer } from 'redux-oidc';
import { toastsReducer } from 'react-toastify-redux';
import {
    PostReducer,
    UserProfileReducer,
} from './reducers';

export default (history) => combineReducers({
    router: connectRouter(history),
    post: PostReducer,
    userProfile: UserProfileReducer,
    oidc: oidcReducer,
    toasts: toastsReducer,
});
