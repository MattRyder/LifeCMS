import React from 'react';
import { useParams } from 'react-router';
import FormPage from 'components/Util/FormPage';
import { useSelector } from 'react-redux';
import { findUserProfile } from 'redux/redux-orm/ORM';
import UserProfileFormComponent from '../../../UserProfile/UserProfileForm/UserProfileFormComponent';
import { useTranslations, useUser } from '../../../../hooks';

export default function UserProfileEdit() {
    const { t, TextTranslationKeys } = useTranslations();

    const { id } = useParams();

    const { userId } = useUser();

    const userProfile = useSelector((state) => findUserProfile(id)(state, userId));

    return (
        <FormPage title={t(TextTranslationKeys.settingsView.userProfiles.edit)}>
            <UserProfileFormComponent userProfile={userProfile} />
        </FormPage>
    );
}
