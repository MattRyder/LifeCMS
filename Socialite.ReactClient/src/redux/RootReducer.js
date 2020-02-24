import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import { reducer as oidcReducer } from 'redux-oidc';
import {
    PostReducer,
    StatusReducer,
    UserReducer,
} from './reducers';

export default (history) => combineReducers({
    router: connectRouter(history),
    profile: combineReducers({
        user: UserReducer,
        status: StatusReducer,
        post: PostReducer,
    }),
    oidc: oidcReducer,
});
