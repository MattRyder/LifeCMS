import React from 'react';
import { Link, useRouteMatch } from 'react-router-dom';
import { useSelector } from 'react-redux';
import UserProfileListViewComponent from '../../../../../UserProfile/UserProfileListViewComponent';
import { useUser, useContentApi, useTranslations } from '../../../../../../hooks';
import { fetchUserProfiles } from '../../../../../../redux/actions/UserProfileActions';

import './UserProfilePaneIndex.scss';

export default function () {
    const { path } = useRouteMatch();

    const { userId, accessToken } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const userProfiles = useSelector((state) => state.userProfile[userId]
        && state.userProfile[userId].userProfiles);

    useContentApi(() => fetchUserProfiles(accessToken, userId), accessToken, userId);

    return (
        <div className="user-profile-pane-index">
            <div className="user-profile-header">
                <Link to={`${path}/new`}>
                    {t(TextTranslationKeys.settingsView.userProfiles.create)}
                </Link>
            </div>

            <UserProfileListViewComponent userProfiles={userProfiles} />
        </div>
    );
}
