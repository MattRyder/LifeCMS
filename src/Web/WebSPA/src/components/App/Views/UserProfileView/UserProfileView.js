import React from 'react';
import {
    Switch, Route, useRouteMatch,
} from 'react-router-dom';
import UserProfilePaneCreate from './UserProfileCreate';
import UserProfilePaneEdit from './UserProfileEdit';
import UserProfileIndex from './UserProfileIndex';

export default function UserProfileView() {
    const { path } = useRouteMatch();

    return (
        <Switch>
            <Route
                path={`${path}/:id/edit`}
                component={UserProfilePaneEdit}
            />
            <Route
                path={`${path}/new`}
                component={UserProfilePaneCreate}
            />
            <Route
                path={`${path}/`}
                component={UserProfileIndex}
            />
        </Switch>
    );
}
