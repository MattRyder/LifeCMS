import React from 'react';
import { Provider } from 'react-redux';
import { Route } from 'react-router';
import { ConnectedRouter } from 'connected-react-router';
import ForgotPasswordForm from 'components/Session/ForgotPasswordForm';
import PasswordResetForm from 'components/Password/PasswordResetForm';
import Layout from 'components/Layout';
import RegistrationFormComponent from './components/Session/Registration/RegistrationForm';
import LoginForm from './components/Session/Login/LoginForm';
import LogoutComponent from './components/Session/Logout/LogoutComponent';
import ErrorForm from './components/Session/Error/ErrorForm';
import configureStore, { history } from './redux/ConfigureStore';

const store = configureStore();

const routes = {
    error: '/accounts/error',
    forgotPassword: '/accounts/forgot-password',
    login: '/accounts/login',
    logout: '/accounts/logout',
    passwordReset: '/password/reset',
    register: '/accounts/register',
};

export default function App() {
    return (
        <Provider store={store}>
            <ConnectedRouter history={history}>
                <Layout>
                    <Route
                        exact
                        path={routes.register}
                        render={() => (
                            <RegistrationFormComponent
                                accountLoginRoute={routes.login}
                                forgotPasswordRoute={routes.forgotPassword}
                            />
                        )}
                    />

                    <Route
                        exact
                        path={routes.login}
                        render={() => (
                            <LoginForm
                                accountRegistrationRoute={routes.register}
                                forgotPasswordRoute={routes.forgotPassword}
                            />
                        )}
                    />

                    <Route
                        exact
                        path={routes.forgotPassword}
                        render={() => (
                            <ForgotPasswordForm
                                accountLoginRoute={routes.login}
                                accountRegistrationRoute={routes.register}
                            />
                        )}
                    />

                    <Route
                        exact
                        path={routes.passwordReset}
                        render={() => (
                            <PasswordResetForm
                                accountLoginRoute={routes.login}
                            />
                        )}
                    />

                    <Route
                        exact
                        path={routes.logout}
                        component={LogoutComponent}
                    />

                    <Route
                        exact
                        path={routes.error}
                        component={ErrorForm}
                    />
                </Layout>
            </ConnectedRouter>
        </Provider>
    );
}
