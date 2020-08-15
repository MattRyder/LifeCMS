import React from 'react';
import {
    Switch, Route, useRouteMatch,
} from 'react-router-dom';
import UserProfilePaneIndex from './UserProfilePaneIndex';
import UserProfilePaneCreate from './UserProfilePaneCreate';
import UserProfilePaneEdit from './UserProfilePaneEdit';

export default function UserProfilesPane() {
    const { path } = useRouteMatch();

    return (
        <div className="user-profile-pane">
            <Switch>
                <Route path={`${path}/new`} component={UserProfilePaneCreate} />
                <Route path={`${path}/:id/edit`} component={UserProfilePaneEdit} />
                <Route path={path} component={UserProfilePaneIndex} />
            </Switch>
        </div>
    );
}
