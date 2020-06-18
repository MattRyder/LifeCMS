import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import { reducer as oidcReducer } from 'redux-oidc';
import {
    PostReducer,
} from './reducers';

export default (history) => combineReducers({
    router: connectRouter(history),
    post: PostReducer,
    oidc: oidcReducer,
});
