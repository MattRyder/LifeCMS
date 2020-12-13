import React from 'react';
import PropTypes from 'prop-types';
import {
    Route, Switch,
} from 'react-router';
import { css, cx } from 'emotion';
import { ToastContainer } from 'react-toastify-redux';
import theme from 'theme';
import AppMeta from 'AppMeta';
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

const styles = {
    appOuter: css`        
        height: 100%;
        font-family: sans-serif;
    `,
    main: css`
        display: flex;
        flex: 1; 
        order: 1;
        width: 100%;
        overflow-y: auto;
        margin-left: 4rem;
        background-color: ${theme.colors.mainBackground};

        @media(min-width: 1199px) {
            margin-left: 0;
        }
    `,
};

export default function App({ productName }) {
    return (
        <>
            <AppMeta title={productName} />
            <div className={cx(styles.appOuter)} id="app-outer">
                <NavigationMenu
                    outerContainerId="app-outer"
                    pageWrapId="page-main"
                />

                <main id="page-main" className={styles.main}>
                    <Switch>
                        <Route path="/profile/:id" component={ProfileView} />
                        <Route path="/session" component={SessionView} />
                        <AuthenticatedRoute exact path="/" component={HomeView} />
                        <AuthenticatedRoute path="/content" component={HomeView} />
                        <AuthenticatedRoute path="/campaigns" component={CampaignView} />
                        <AuthenticatedRoute path="/templates" component={TemplatesView} />
                        <AuthenticatedRoute path="/user-profiles" component={UserProfileView} />
                    </Switch>
                    <ToastContainer position="bottom-right" />
                </main>
            </div>
        </>
    );
}

App.propTypes = {
    productName: PropTypes.string.isRequired,
};
