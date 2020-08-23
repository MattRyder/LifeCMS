import React from 'react';
import {
    Switch, Route, useRouteMatch,
} from 'react-router-dom';
import UserProfilePaneCreate from './UserProfileCreate';
import UserProfilePaneEdit from './UserProfileEdit';

export default function UserProfileView() {
    const { path } = useRouteMatch();

    return (
        <Switch>
            <Route path={`${path}/new`} component={UserProfilePaneCreate} />
            <Route path={`${path}/:id/edit`} component={UserProfilePaneEdit} />
        </Switch>
    );
}
