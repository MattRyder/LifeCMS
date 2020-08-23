import React from 'react';
import { useSelector } from 'react-redux';
import { Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import { fetchUserProfiles } from '../../../../../redux/actions/UserProfileActions';
import { useContentApi, useUser, useTranslations } from '../../../../../hooks';
import UserProfileListViewRowComponent from '../../../../UserProfile/UserProfileListViewRowComponent';
import TableComponent from '../../../../Util/Table/TableComponent';

import './UserProfileIndex.scss';

export default function UserProfileIndex() {
    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const userProfileState = useSelector(
        (state) => state.userProfile[userId],
    );

    useContentApi(() => fetchUserProfiles(accessToken, userId), accessToken, userId);

    return (
        <div className="user-profile-index">
            <div className="title">
                <h2>{t(TextTranslationKeys.settingsView.menu.userProfiles)}</h2>
                <Button tag={Link} size="sm" color="primary" to="/user-profiles/new">
                    {t(TextTranslationKeys.settingsView.userProfiles.create)}
                </Button>
            </div>
            <TableComponent
                headings={['Name', 'Occupation', 'Location']}
                rowComponent={UserProfileListViewRowComponent}
                collection={userProfileState && userProfileState.userProfiles}
            />
        </div>
    );
}
