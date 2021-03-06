import React from 'react';
import { Route } from 'react-router';
import { useUser } from '../../hooks';
import UserManager from '../../openid/UserManager';

export default function AuthenticatedRoute({ component: Component, ...otherProps }) {
    const { userId } = useUser();

    if (!userId) {
        UserManager.signinRedirect();
    }

    return (
        <Route
            {...otherProps}
            render={(props) => (userId
                ? <Component {...props} />
                : null
            )}
        />
    );
}
