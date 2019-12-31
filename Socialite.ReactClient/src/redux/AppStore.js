import { createStore, applyMiddleware, compose } from 'redux';
import { createBrowserHistory } from 'history';
import createRootReducer from './RootReducer';
import { routerMiddleware } from 'connected-react-router';
import thunk from 'redux-thunk';

export const history = createBrowserHistory();

export const routingMiddleware = routerMiddleware(history);

export default function configureStore(preloadedState) {
    const store = createStore(
        createRootReducer(history),
        preloadedState,
        compose(
            applyMiddleware(thunk),
            applyMiddleware(routingMiddleware),
        ),
    );

    return store;
}