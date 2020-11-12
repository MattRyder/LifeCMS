import React from 'react';
import FormPage from 'components/Util/FormPage';
import UserProfileFormComponent from '../../../UserProfile/UserProfileForm/UserProfileFormComponent';
import { useTranslations } from '../../../../hooks';

export default function UserProfileCreate() {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <FormPage title={t(TextTranslationKeys.settingsView.userProfiles.create)}>
            <UserProfileFormComponent />
        </FormPage>
    );
}
