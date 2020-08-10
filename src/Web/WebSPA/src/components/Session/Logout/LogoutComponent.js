import React, { useEffect } from 'react';
import userManager from '../../../openid/UserManager';

import '../Session.scss';

export default function LogoutComponent() {
    useEffect(() => userManager.signoutRedirect());

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
