import React from 'react';
import { storiesOf } from '@storybook/react';
import UserProfileComponent from '../components/UserProfile/UserProfileComponent';
import { createUserProfile } from './factories/factories';
import UserProfileListViewComponent from '../components/UserProfile/UserProfileListViewComponent';
import CreateUserProfileComponent from '../components/UserProfile/CreateUserProfile/CreateUserProfileComponent';

const {
    name, avatarImageUrl, headerImageUrl, occupation, location,
} = createUserProfile();

const userProfiles = [...Array(3)].map(() => createUserProfile());

storiesOf('UserProfile', module)
    .add('UserProfileComponent', () => (
        <UserProfileComponent
            name={name}
            avatarImageUrl={avatarImageUrl}
            headerImageUrl={headerImageUrl}
            occupation={occupation}
            location={location}
        />
    ))
    .add('UserProfileListViewComponent', () => (
        <UserProfileListViewComponent userProfiles={userProfiles} match={{ path: 'storybook/user-profiles-list-view-component'}} />
    ))
    .add('CreateUserProfileComponent', () => (
        <CreateUserProfileComponent />
    ));
