import React from 'react';
import { CallbackComponent as ReduxOidcCallbackComponent } from 'redux-oidc';
import { push } from 'connected-react-router';
import { useDispatch } from 'react-redux';
import userManager from '../../openid/UserManager';

export default function CallbackComponent() {
    const dispatch = useDispatch();

    return (
        <div className="openid-connect-callback">
            <ReduxOidcCallbackComponent
                userManager={userManager}
                successCallback={() => dispatch(push('/'))}
                errorCallback={(error) => { throw error; }}
            >
                <div />
            </ReduxOidcCallbackComponent>
        </div>
    );
}
