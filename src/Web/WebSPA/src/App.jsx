import React from 'react';
import { Route, Switch, Redirect } from 'react-router';
import AppTopNaviationComponent from './components/App/Components/AppTopNavigation/AppTopNavigationComponent';
import { SessionView, HomeView, ProfileView } from './components/App/Views';

import 'bootstrap/dist/css/bootstrap.css';
import './App.scss';

export default function () {
    return (
        <div>
            <AppTopNaviationComponent />
            <Switch>
                <Route exact path="/">
                    <Redirect to="/news-feed" />
                </Route>
                <Route path="/profile/:id" component={ProfileView} />
                <Route path="/news-feed" component={HomeView} />
                <Route path="/session" component={SessionView} />
            </Switch>
        </div>
    );
}
