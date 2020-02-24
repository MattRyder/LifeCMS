import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'connected-react-router';
import { loadUser, OidcProvider } from 'redux-oidc';
import App from './App';
import * as serviceWorker from './serviceWorker';
import configureStore, { history } from './redux/AppStore';
import userManager from './openid/UserManager';

const rootElement = document.getElementById('root');

const store = configureStore();

loadUser(store, userManager);

ReactDOM.render(
    <Provider store={store}>
        <OidcProvider store={store} userManager={userManager}>
            <ConnectedRouter history={history}>
                <App />
            </ConnectedRouter>
        </OidcProvider>
    </Provider>,
    rootElement,
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
