import { createStore, applyMiddleware, compose } from 'redux';
import { createBrowserHistory } from 'history';
import { routerMiddleware } from 'connected-react-router';
import thunk from 'redux-thunk';
import createRootReducer from './RootReducer';
import RuntimeConfiguration from './middlewares/signalr/RuntimeConfiguration';
import signalrMiddleware from './middlewares/signalr/SignalrMiddleware';
import { fetchPosts } from './actions/PostActions';

export const history = createBrowserHistory();

export const router = routerMiddleware(history);

const signalr = signalrMiddleware({
    url: RuntimeConfiguration.json().websocket_host,
    eventCallbacks: [
        {
            eventName: 'PostPublished',
            eventCallback: (userId) => (getState, dispatch) => {
                const { oidc: { user } } = getState();

                if (user) {
                    dispatch(fetchPosts(user.access_token, userId));
                }
            },
        },
    ],
});

const composeEnhancers = (typeof window !== 'undefined' && window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__) || compose;

export default function configureStore(preloadedState) {
    const store = createStore(
        createRootReducer(history),
        preloadedState,
        composeEnhancers(
            applyMiddleware(thunk),
            applyMiddleware(router),
            applyMiddleware(signalr),
        ),
    );

    return store;
}
