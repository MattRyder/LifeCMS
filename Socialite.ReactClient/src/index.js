import React from 'react';
import ReactDOM from 'react-dom';

import { Provider } from 'react-redux';
import { ConnectedRouter } from 'connected-react-router';

import App from './App';
import * as serviceWorker from './serviceWorker';
import configureStore, { history } from './redux/AppStore';
import { loadUser } from 'redux-oidc';
import userManager from './openid/UserManager';

const rootElement = document.getElementById('root');

const store = configureStore();

loadUser(store, userManager);

ReactDOM.render(
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <App />
        </ConnectedRouter>
    </Provider>,
    rootElement);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
