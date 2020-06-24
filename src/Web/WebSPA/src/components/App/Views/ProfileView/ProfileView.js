import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useUser } from '../../../../hooks';
import UserProfileComponent from '../../../UserProfile/UserProfileComponent';
import { fetchUserProfile } from '../../../../redux/actions/UserProfileActions';

import './ProfileView.scss';

export default function ({ match: { params: { id: userId } } }) {
    const dispatch = useDispatch();

    const [isLoaded, setIsLoaded] = useState(false);

    const { accessToken } = useUser();

    const userProfileState = useSelector((state) => state.userProfile[userId] && state.userProfile[userId].userProfile);

    useEffect(() => {
        if (!isLoaded) {
            dispatch(fetchUserProfile(accessToken, userId));
            setIsLoaded(true);
        }
    }, [dispatch, isLoaded, accessToken, userId]);

    const {
        name,
        occupation,
        location,
        avatarImageUri,
        headerImageUri,
    } = userProfileState || {};

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
