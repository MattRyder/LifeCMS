import React, { Component } from 'react';
import { Provider } from 'react-redux';
import { Route, withRouter } from 'react-router';
import { ConnectedRouter } from 'connected-react-router'
import { Layout } from './components/Layout';
import RegistrationForm from './components/Session/Registration/RegistrationForm';
import LoginForm from './components/Session/Login/LoginForm';
import LogoutComponent from './components/Session/Logout/LogoutComponent';
import ErrorForm from './components/Session/Error/ErrorForm';
import configureStore, { history } from './redux/ConfigureStore';

const store = configureStore();

class App extends Component {
  render() {
    return (
      <Provider store={store}>
        <ConnectedRouter history={history} >
          <Layout>
            <Route exact path='/Accounts/Register' component={RegistrationForm} />
            <Route exact path='/Accounts/Login' component={LoginForm} />
            <Route exact path='/Accounts/Logout' component={LogoutComponent} />
            <Route exact path='/Accounts/Error' component={ErrorForm} />
          </Layout>
        </ConnectedRouter>
      </Provider>
    );
  }
}

export default App;