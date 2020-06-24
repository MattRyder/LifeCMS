import React from 'react';
import { storiesOf } from '@storybook/react';

import AppTopNavigationComponent from '../components/App/Components/AppTopNavigation/AppTopNavigationComponent';
import ProfileView from '../components/App/Views/ProfileView/ProfileView';
import { CallbackComponent } from '../components/Session/CallbackComponent';
import { createUserProfile } from './factories/factories';

import '../assets/styles/colors.scss';
import SettingsView from '../components/App/Views/SettingsView/SettingsView';

storiesOf('App/AppTopNavigation', module)
    .add('with unauthenticated user', () => <AppTopNavigationComponent />);

storiesOf('App/Views', module)
    .add('ProfileView', () => <ProfileView userProfile={createUserProfile()} match={{params: {id: 1}}} />)
    .add('SettingsView', () => <SettingsView menuItemGroups={[]} />);

storiesOf('App/Components', module)
    .add('CallbackComponent', () => <CallbackComponent />);
