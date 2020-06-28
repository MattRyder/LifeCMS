import React from 'react';
import { Route, Switch, Redirect, useRouteMatch } from 'react-router';
import { ToastContainer } from 'react-toastify-redux';
import AppTopNaviationComponent from './components/App/Components/AppTopNavigation/AppTopNavigationComponent';
import {
    SessionView, HomeView, ProfileView, SettingsView,
} from './components/App/Views';
import { useUser } from './hooks';


import 'bootstrap/dist/css/bootstrap.css';
import 'react-toastify/dist/ReactToastify.css';
import './App.scss';

export default function () {
    const match = useRouteMatch();

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
                <Route
                    path="/settings"
                    render={() => <SettingsView match={match} />}
                />
            </Switch>
            <ToastContainer position="bottom-right" />
        </div>
    );
}
