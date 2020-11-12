import React from 'react';
import {
    Route, Switch,
} from 'react-router';
import { ToastContainer } from 'react-toastify-redux';
import {
    SessionView,
    HomeView,
    ProfileView,
    UserProfileView,
    TemplatesView,
    CampaignView,
} from './components/App/Views';
import AuthenticatedRoute from './components/Util/AuthenticatedRoute';
import NavigationMenu from './components/App/Components/NavigationMenu/NavigationMenu';

import 'bootstrap/dist/css/bootstrap.css';
import 'react-toastify/dist/ReactToastify.css';
import './App.scss';

export default function App() {
    return (
        <div id="app-outer">
            <NavigationMenu outerContainerId="app-outer" pageWrapId="page-main" />

            <main id="page-main" style={{ display: 'flex', flex: 1 }}>
                <Switch>
                    <Route path="/profile/:id" component={ProfileView} />
                    <Route path="/session" component={SessionView} />
                    <AuthenticatedRoute path="/content" component={HomeView} />
                    <AuthenticatedRoute path="/campaigns" component={CampaignView} />
                    <AuthenticatedRoute path="/templates" component={TemplatesView} />
                    <AuthenticatedRoute path="/user-profiles" component={UserProfileView} />
                </Switch>
                <ToastContainer position="bottom-right" />
            </main>
        </div>
    );
}
