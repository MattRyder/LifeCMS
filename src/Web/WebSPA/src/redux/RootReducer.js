import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import { reducer as oidcReducer } from 'redux-oidc';
import { toastsReducer } from 'react-toastify-redux';
import { createReducer } from 'redux-orm';
import ORM from './redux-orm/ORM';

export default (history) => combineReducers({
    router: connectRouter(history),
    orm: createReducer(ORM),
    oidc: oidcReducer,
    toasts: toastsReducer,
});
