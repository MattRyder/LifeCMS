import React, { Component } from 'react';
import { Provider } from 'react-redux';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import RegistrationForm from './components/Session/Registration/RegistrationForm';
import LoginForm from './components/Session/Login/LoginForm';
import ErrorForm from './components/Session/Error/ErrorForm';
import configureStore from './redux/ConfigureStore';

const store = configureStore();

export default class App extends Component {
  render() {
    return (
      <Provider store={store}>
        <Layout>
          <Route exact path='/Accounts/Register' component={RegistrationForm} />
          <Route exact path='/Accounts/Login' component={LoginForm} />
          <Route exact path='/Accounts/Error' component={ErrorForm} />
        </Layout>
      </Provider>
    );
  }
}
