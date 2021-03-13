import React from 'react';
import FormPage from 'components/Util/FormPage';
import { useDispatch } from 'react-redux';
import { createUserProfile } from 'redux/actions/UserProfileActions';
import UserProfileFormComponent from '../../../UserProfile/UserProfileForm/UserProfileFormComponent';
import { useTranslations, useUser } from '../../../../hooks';

export default function UserProfileCreate() {
    const { t, TextTranslationKeys } = useTranslations();

    const dispatch = useDispatch();

    const { accessToken } = useUser();

    const onSave = (params) => dispatch(createUserProfile(
        accessToken,
        params,
        '/user-profiles',
    ));

    return (
        <FormPage
            title={t(TextTranslationKeys.userProfileView.create.pageTitle)}
        >
            <UserProfileFormComponent handleSave={onSave} />
        </FormPage>
    );
}
