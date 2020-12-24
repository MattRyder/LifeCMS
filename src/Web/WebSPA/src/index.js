import React from 'react';
import ReactDOM from 'react-dom';
import AppRuntimeConfiguration from 'AppRuntimeConfiguration';
import './i18n';
import * as serviceWorker from './serviceWorker';
import configureStore, { history } from './redux/AppStore';
import userManager from './openid/UserManager';
import ProviderWrapper from './Provider';
import App from './App';

const rootElement = document.getElementById('root');

const { store, persistor } = configureStore();

const RUNTIME_PRODUCT_NAME = AppRuntimeConfiguration.json().product_name;

ReactDOM.render(
    <ProviderWrapper
        store={store}
        userManager={userManager}
        history={history}
        persistor={persistor}
    >
        <App productName={RUNTIME_PRODUCT_NAME} />
    </ProviderWrapper>,
    rootElement,
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
