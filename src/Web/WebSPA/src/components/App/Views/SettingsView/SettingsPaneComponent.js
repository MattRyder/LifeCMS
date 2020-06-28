import React from 'react';
import {
    Route, Switch, useRouteMatch,
} from 'react-router-dom';
import UserProfilesPane from './panes/UserProfilesPane/UserProfilesPane';

export default function () {
    const { path } = useRouteMatch();

    return (
        <div className="settings-pane-component">
            <Switch>
                <Route path={`${path}/user-profiles`} component={UserProfilesPane} />
            </Switch>
        </div>
    );
}
