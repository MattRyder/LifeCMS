import React from 'react';
import {
    Route, Switch,
} from 'react-router';
import { ToastContainer } from 'react-toastify-redux';
import {
    SessionView,
    HomeView,
    ProfileView,
    SettingsView,
    NewsletterView,
    UserProfileView,
    NewsletterTemplatesView,
} from './components/App/Views';
import AuthenticatedRoute from './components/Util/AuthenticatedRoute';

import 'bootstrap/dist/css/bootstrap.css';
import 'react-toastify/dist/ReactToastify.css';
import './App.scss';
import NavigationMenu from './components/App/Components/NavigationMenu/NavigationMenu';

export default function App() {
    return (
        <div id="app-outer">
            <NavigationMenu outerContainerId="app-outer" pageWrapId="page-main" />

            <main id="page-main" style={{ display: 'flex', flex: 1 }}>
                <Switch>
                    <Route path="/profile/:id" component={ProfileView} />
                    <Route path="/session" component={SessionView} />
                    <AuthenticatedRoute path="/content" component={HomeView} />
                    <AuthenticatedRoute path="/newsletter" component={NewsletterView} />
                    <AuthenticatedRoute path="/templates" component={NewsletterTemplatesView} />
                    <AuthenticatedRoute path="/user-profiles" component={UserProfileView} />
                    <AuthenticatedRoute path="/settings" component={SettingsView} />
                </Switch>
                <ToastContainer position="bottom-right" />
            </main>
        </div>
    );
}
