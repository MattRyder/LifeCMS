import React from 'react';
import UserProfileFormComponent from '../../../../../UserProfile/UserProfileForm/UserProfileFormComponent';
import { useTranslations } from '../../../../../../hooks';

import './UserProfilePaneCreate.scss';

export default function () {
    const { t, TextTranslationKeys } = useTranslations();
    return (
        <div className="user-profile-pane-new">
            <div className="user-profile-header">
                <span>{t(TextTranslationKeys.settingsView.userProfiles.create)}</span>
            </div>

            <UserProfileFormComponent />
        </div>
    );
}
