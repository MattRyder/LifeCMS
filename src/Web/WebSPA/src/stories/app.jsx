import React from 'react';
import { storiesOf } from '@storybook/react';

import AppTopNavigationComponent from '../components/App/Components/AppTopNavigation/AppTopNavigationComponent';
// import UserProfileComponent from '../components/UserProfile/UserProfileComponent';
import { CallbackComponent } from '../components/Session/CallbackComponent';
// import { createUser } from './factories/factories';

import '../assets/styles/colors.scss';

storiesOf('App/AppTopNavigation', module)
    .add('with unauthenticated user', () => <AppTopNavigationComponent />);

// storiesOf('App/Views', module)
//     .add('ProfileView', () => <ProfileView userProfile={createUser()} />);

storiesOf('App/Components', module)
    .add('CallbackComponent', () => <CallbackComponent />);
