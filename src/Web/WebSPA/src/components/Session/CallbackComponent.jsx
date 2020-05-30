import React from 'react';
import { CallbackComponent as ReduxOidcCallbackComponent } from 'redux-oidc';
import { push } from 'connected-react-router';
import { connect } from 'react-redux';
import userManager from '../../openid/UserManager';

const mapDispatchToProps = (dispatch) => ({
    redirectToHome: () => dispatch(push('/profile')),
    redirectToError: () => dispatch(push('/session/oauth_error')),
});

export function CallbackComponent({ redirectToHome, redirectToError }) {
    return (
        <div className="openid-connect-callback">
            <ReduxOidcCallbackComponent
                userManager={userManager}
                successCallback={() => redirectToHome()}
                errorCallback={(error) => redirectToError(error)}
            >
                <div />
            </ReduxOidcCallbackComponent>
        </div>
    );
}

export default connect(null, mapDispatchToProps)(CallbackComponent);
