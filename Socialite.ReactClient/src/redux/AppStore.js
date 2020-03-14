import { createStore, applyMiddleware, compose } from 'redux';
import { createBrowserHistory } from 'history';
import { routerMiddleware } from 'connected-react-router';
import thunk from 'redux-thunk';
import createRootReducer from './RootReducer';

export const history = createBrowserHistory();

export const routingMiddleware = routerMiddleware(history);

const composeEnhancers = (typeof window !== 'undefined' && window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__) || compose;

export default function configureStore(preloadedState) {
    const store = createStore(
        createRootReducer(history),
        preloadedState,
        composeEnhancers(
            applyMiddleware(thunk),
            applyMiddleware(routingMiddleware),
        ),
    );

    return store;
}
