import React from 'react';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'connected-react-router';
import { OidcProvider, loadUser } from 'redux-oidc';
import { PersistGate } from 'redux-persist/integration/react';

export default ({
    children, store, persistor, userManager, history,
}) => {
    loadUser(store, userManager);

    return (
        <Provider store={store}>
            <PersistGate loading={null} persistor={persistor}>
                <OidcProvider store={store} userManager={userManager}>
                    <ConnectedRouter history={history}>
                        {children}
                    </ConnectedRouter>
                </OidcProvider>
            </PersistGate>
        </Provider>
    );
};
