import React from 'react';
import {
    useRouteMatch, Switch, Route, Redirect,
} from 'react-router';
import UserProfileIndex from './UserProfile/UserProfileIndex';

export default function () {
    const { path } = useRouteMatch();

    return (
        <div style={{ padding: '2rem', width: '100%' }}>
            <Switch>
                <Route path={`${path}/user-profiles`} component={UserProfileIndex} />
                <Route exact path={`${path}`}>
                    <Redirect to={`${path}/user-profiles`} />
                </Route>
            </Switch>
        </div>
    );
}
