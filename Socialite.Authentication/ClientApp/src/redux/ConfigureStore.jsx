import { createBrowserHistory } from 'history';
import { applyMiddleware, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerMiddleware } from 'connected-react-router';
import createRootReducer from './reducers/Index';

export const history = createBrowserHistory();

export default function configureStore(preloadedState) {
    const composeEnhancers = (typeof window !== 'undefined' && window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__) || compose;

    return createStore(
        createRootReducer(history),
        preloadedState,
        composeEnhancers(
            applyMiddleware(
                routerMiddleware(history),
            ),
            applyMiddleware(
                thunk,
            )
        )
    )
}