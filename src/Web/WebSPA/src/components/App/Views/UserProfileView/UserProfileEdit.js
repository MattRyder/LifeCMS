import React from 'react';
import { useParams } from 'react-router';
import FormPage from 'components/Util/FormPage';
import UserProfileFormComponent from '../../../UserProfile/UserProfileForm/UserProfileFormComponent';
import { useTranslations, useUser, useStateSelector } from '../../../../hooks';

export default function UserProfileEdit() {
    const { t, TextTranslationKeys } = useTranslations();

    const { id } = useParams();

    const { userId } = useUser();

    const userProfile = useStateSelector(
        userId,
        'userProfile',
        'userProfiles',
        id,
    );

    return (
        <FormPage title={t(TextTranslationKeys.settingsView.userProfiles.edit)}>
            <UserProfileFormComponent userProfile={userProfile} />
        </FormPage>
    );
}
