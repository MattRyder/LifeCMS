import React from 'react';
import UserProfileFormComponent from '../../../UserProfile/UserProfileForm/UserProfileFormComponent';
import { useTranslations } from '../../../../hooks';

import './UserProfileCreate.scss';

export default function UserProfileCreate() {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <div className="user-profile-create">
            <div>
                <div className="user-profile-header">
                    <span>
                        {t(TextTranslationKeys.settingsView.userProfiles.create)}
                    </span>
                </div>

                <UserProfileFormComponent />
            </div>
        </div>
    );
}
