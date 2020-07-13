import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import './i18n';
import * as serviceWorker from './serviceWorker';
import configureStore, { history } from './redux/AppStore';
import userManager from './openid/UserManager';
import ProviderWrapper from './Provider';

const rootElement = document.getElementById('root');

const persistedState = localStorage.getItem('reduxState')
    ? JSON.parse(localStorage.getItem('reduxState'))
    : {};

const store = configureStore(persistedState);

store.subscribe(() => {
    localStorage.setItem(
        'reduxState',
        JSON.stringify(store.getState()),
    );
});

ReactDOM.render(
    <ProviderWrapper store={store} userManager={userManager} history={history}>
        <App />
    </ProviderWrapper>,
    rootElement,
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
