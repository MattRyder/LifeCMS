import React from 'react';
import { storiesOf } from '@storybook/react';
import UserProfileComponent from '../components/UserProfile/UserProfileComponent';
import { createUserProfile } from './factories/factories';

const {
    name, avatarImageUrl, headerImageUrl, occupation, location,
} = createUserProfile();

storiesOf('UserProfile', module)
    .add('UserProfileComponent', () => (
        <UserProfileComponent
            name={name}
            avatarImageUrl={avatarImageUrl}
            headerImageUrl={headerImageUrl}
            occupation={occupation}
            location={location}
        />
    ));
