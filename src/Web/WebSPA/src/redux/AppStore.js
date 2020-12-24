import { createStore, applyMiddleware, compose } from 'redux';
import { createBrowserHistory } from 'history';
import { routerMiddleware } from 'connected-react-router';
import { persistStore, persistReducer } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import thunk from 'redux-thunk';
import createRootReducer from './RootReducer';
import RuntimeConfiguration from './middlewares/signalr/RuntimeConfiguration';
import { SignalrMiddleware } from './middlewares';
import { fetchPosts } from './actions/PostActions';

const persistConfig = {
    key: 'root',
    storage,
};

export const history = createBrowserHistory();

export const router = routerMiddleware(history);

const signalr = SignalrMiddleware({
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

const persistedReducer = persistReducer(
    persistConfig,
    createRootReducer(history),
);

export default function configureStore(preloadedState) {
    const store = createStore(
        persistedReducer,
        preloadedState,
        composeEnhancers(
            applyMiddleware(thunk),
            applyMiddleware(router),
            applyMiddleware(signalr),
        ),
    );

    const persistor = persistStore(store);

    return { store, persistor };
}
