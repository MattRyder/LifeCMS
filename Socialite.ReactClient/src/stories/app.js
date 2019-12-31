import React from 'react';
import { storiesOf } from "@storybook/react";

import AppHeaderComponent from '../components/App/AppHeaderComponent';

import '../assets/styles/colors.scss';
import ProfileView from '../components/App/Views/ProfileView';
import { ActionMenuItem } from '../components/Profile/BasicInfoComponent';
import { createUser, createStatus } from './factories/factories';
import MenuComponent from '../components/App/Components/MenuComponent';

const actionMenuItems = [
    new ActionMenuItem("Connect", "#connect"),
    new ActionMenuItem("Edit Details", "#edit")
];

const menuItems = [
    new ActionMenuItem("Statuses", "/profile/statuses"),
    new ActionMenuItem("Posts", "/profile/posts"),
    new ActionMenuItem("Photos", "/profile/photos"),
];

const createStatusArray = () => [...Array(10).keys()].map(createStatus);

storiesOf('App/AppHeader', module)
    .add('with unauthenticated user', () => <AppHeaderComponent />);

storiesOf('App/Views', module)
    .add('ProfileView', () => <ProfileView userProfile={createUser()} actionMenuItems={actionMenuItems} statuses={createStatusArray()} />);

storiesOf('App/Components', module)
    .add('Menu', () => <MenuComponent menuItems={menuItems} />);