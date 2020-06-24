import React from 'react';
import {
    Route, Switch,
} from 'react-router-dom';

export default function ({ path }) {
    return (
        <div className="settings-pane-component">
            <Switch>
                <Route path={`${path}/user-profiles`}>
                    <h1>User Profiles</h1>
                </Route>
            </Switch>
        </div>
    );
}
