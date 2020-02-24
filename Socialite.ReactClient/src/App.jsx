import React from 'react';
import { Route, Switch } from 'react-router';
import AppHeaderComponent from './components/App/Components/AppHeaderComponent';
import { ProfileView, SessionView } from './components/App/Views';

import 'bootstrap/dist/css/bootstrap.css';
import './App.scss';

export default function () {
    return (
        <div>
            <AppHeaderComponent />
            <Switch>
                <Route path="/profile/:id" component={ProfileView} />
                <Route path="/session" component={SessionView} />
            </Switch>
        </div>
    );
}
