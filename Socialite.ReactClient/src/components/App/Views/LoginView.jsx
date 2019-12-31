import React, { Component } from 'react';
import userManager from '../../../openid/UserManager';

class LoginView extends Component {
    onLoginButtonClick(e) {
        e.preventDefault();

        userManager.signinRedirect();
    }

    render() {
        return (
            <div className="login-view">
                <button onClick={this.onLoginButtonClick}>Login</button>
            </div>
        );
    }
}

export default LoginView;