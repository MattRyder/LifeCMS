import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Container } from 'reactstrap';

import AppHeaderComponent from './components/App/Components/AppHeaderComponent';

import 'bootstrap/dist/css/bootstrap.css';
import './App.scss';
import ProfileView from './components/App/Views/ProfileView';
import LoginView from './components/App/Views/LoginView';

class App extends Component {
  render() {
    return (
      <div>
        <AppHeaderComponent />
        <Container>
          <Switch>
            <Route path='/profile/:id' component={ProfileView}></Route>
            <Route exact path='/login' component={LoginView}></Route>
          </Switch>
        </Container>
      </div>
    );
  }
}

export default App;
