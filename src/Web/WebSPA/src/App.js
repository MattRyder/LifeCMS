import React from 'react';
import {
    Route, Switch, Redirect,
} from 'react-router';
import { ToastContainer } from 'react-toastify-redux';
import AppTopNaviationComponent from './components/App/Components/AppTopNavigation/AppTopNavigationComponent';
import {
    SessionView,
    HomeView,
    ProfileView,
    SettingsView,
    NewsletterView,
} from './components/App/Views';
import AuthenticatedRoute from './components/Util/AuthenticatedRoute';

import 'bootstrap/dist/css/bootstrap.css';
import 'react-toastify/dist/ReactToastify.css';
import './App.scss';

export default function () {
    return (
        <div>
            <AppTopNaviationComponent />
            <Switch>
                <Route exact path="/">
                    <Redirect to="/my-story" />
                </Route>
                <Route path="/profile/:id" component={ProfileView} />
                <Route path="/session" component={SessionView} />
                <AuthenticatedRoute path="/my-story" component={HomeView} />
                <AuthenticatedRoute path="/newsletters" component={NewsletterView} />
                <AuthenticatedRoute path="/settings" component={SettingsView} />
            </Switch>
            <ToastContainer position="bottom-right" />
        </div>
    );
}
