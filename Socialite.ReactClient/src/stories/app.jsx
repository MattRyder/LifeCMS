import React from 'react';
import { storiesOf } from '@storybook/react';

import AppTopNavigationComponent from '../components/App/Components/AppTopNavigationComponent';
import { CallbackComponent } from '../components/Session/CallbackComponent';

import '../assets/styles/colors.scss';
import ProfileView from '../components/App/Views/ProfileView';
import { ActionMenuItem } from '../components/Profile/BasicInfoComponent';
import { createUser, createStatus } from './factories/factories';
import MenuComponent from '../components/App/Components/MenuComponent';

const actionMenuItems = [
    new ActionMenuItem('Connect', '#connect'),
    new ActionMenuItem('Edit Details', '#edit'),
];

const menuItems = [
    new ActionMenuItem('Statuses', '/profile/statuses'),
    new ActionMenuItem('Posts', '/profile/posts'),
    new ActionMenuItem('Photos', '/profile/photos'),
];

const createStatusArray = () => [...Array(10).keys()].map(createStatus);

storiesOf('App/AppTopNavigation', module)
    .add('with unauthenticated user', () => <AppTopNavigationComponent />);

storiesOf('App/Views', module)
    .add('ProfileView', () => (
        <ProfileView
            userProfile={createUser()}
            actionMenuItems={actionMenuItems}
            statuses={createStatusArray()}
            match={{
                url: '/profile-view-story',
                params: {
                    id: 'e5c64dd1-88e0-4fa0-a35f-83adb39c6e1f',
                },
            }}
        />
    ));

storiesOf('App/Components', module)
    .add('Menu', () => <MenuComponent menuItems={menuItems} />)
    .add('CallbackComponent', () => <CallbackComponent />);
