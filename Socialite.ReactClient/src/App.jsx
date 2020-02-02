import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Container } from 'reactstrap';

import AppHeaderComponent from './components/App/Components/AppHeaderComponent';

import 'bootstrap/dist/css/bootstrap.css';
import './App.scss';
import ProfileView from './components/App/Views/ProfileView';
import CallbackComponent from './components/App/Components/CallbackComponent';

class App extends Component {
  render() {
    return (
      <div>
        <AppHeaderComponent />
        <Container>
          <Switch>
            <Route exact path='/OAuth2/Callback' component={CallbackComponent} />
            <Route path='/profile/:id' component={ProfileView}></Route>
          </Switch>
        </Container>
      </div>
    );
  }
}

export default App;
