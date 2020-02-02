import React from 'react';
import { CallbackComponent as ReduxOidcCallbackComponent } from 'redux-oidc';
import { push } from 'connected-react-router';
import { connect } from 'react-redux';
import userManager from '../../../openid/UserManager';

const mapDispatchToProps = (dispatch) => ({
    redirectHome: () => dispatch(push),
})

function CallbackComponent({ redirectHome }) {
    return (
        <ReduxOidcCallbackComponent
            userManager={userManager}
            successCallback={() => redirectHome()}
            errorCallback={(error) => {
                redirectHome();

                console.error(error);
            }}
        >
            <div>Redirecting...</div>
        </ReduxOidcCallbackComponent>
    );
}

export default connect(null, mapDispatchToProps)(CallbackComponent);
