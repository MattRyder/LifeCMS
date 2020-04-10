import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import { reducer as oidcReducer } from 'redux-oidc';
import {
    PostReducer,
    StatusReducer,
} from './reducers';

export default (history) => combineReducers({
    router: connectRouter(history),
    status: StatusReducer,
    post: PostReducer,
    oidc: oidcReducer,
});
