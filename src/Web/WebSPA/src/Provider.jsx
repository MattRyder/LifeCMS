import React from 'react';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'connected-react-router';
import { OidcProvider, loadUser } from 'redux-oidc';

export default ({ children, store, userManager, history }) => {
    loadUser(store, userManager);

    return (
        <Provider store={store}>
            <OidcProvider store={store} userManager={userManager}>
                <ConnectedRouter history={history}>
                    {children}
                </ConnectedRouter>
            </OidcProvider>
        </Provider>
    );
};
