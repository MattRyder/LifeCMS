import React, { Component } from 'react';
import userManager from '../../../openid/UserManager';

import '../Session.scss';

class LogoutComponent extends Component {
    componentDidMount() {
        userManager.signoutRedirect();
    }

    render() {
        return (
            <div className="logout">
                <h2>You have been signed out.</h2>
                <p>
                    To go back to LifeCMS,
                    <a href="/">click here.</a>
                </p>
            </div>
        );
    }
}

export default LogoutComponent;
