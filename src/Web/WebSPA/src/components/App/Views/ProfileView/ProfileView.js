import React from 'react';
import { useSelector } from 'react-redux';
import { useUser, useContentApi } from '../../../../hooks';
import UserProfileComponent from '../../../UserProfile/UserProfileComponent';
import { fetchUserProfiles } from '../../../../redux/actions/UserProfileActions';

import './ProfileView.scss';

export default function ({ match: { params: { id: userId } } }) {
    const { accessToken } = useUser();

    const userProfilesState = useSelector(
        (state) => state.userProfile[userId] && state.userProfile[userId].userProfiles,
    );

    useContentApi(() => fetchUserProfiles(accessToken, userId), accessToken, userId);

    const {
        name,
        occupation,
        location,
        avatarImageUri,
        headerImageUri,
    } = userProfilesState || {};

    return (
        <div className="profile-view">
            <UserProfileComponent
                name={name}
                occupation={occupation}
                location={location}
                avatarImageUri={avatarImageUri}
                headerImageUri={headerImageUri}
            />
            <div className="user-activity-component" />
        </div>
    );
}
