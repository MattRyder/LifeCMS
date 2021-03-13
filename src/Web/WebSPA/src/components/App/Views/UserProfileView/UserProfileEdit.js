import React from 'react';
import { useParams } from 'react-router';
import FormPage from 'components/Util/FormPage';
import { useDispatch, useSelector } from 'react-redux';
import { findUserProfile } from 'redux/redux-orm/ORM';
import { editUserProfile } from 'redux/actions/UserProfileActions';
import UserProfileFormComponent from '../../../UserProfile/UserProfileForm/UserProfileFormComponent';
import { useTranslations, useUser } from '../../../../hooks';

export default function UserProfileEdit() {
    const { t, TextTranslationKeys } = useTranslations();

    const { id } = useParams();

    const { userId } = useUser();

    const dispatch = useDispatch();

    const { accessToken } = useUser();

    const onSave = (params) => {
        const paramsWithId = params;

        paramsWithId.id = id;

        dispatch(editUserProfile({
            accessToken,
            params,
            redirectTo: '/user-profiles',
        }));
    };

    const userProfile = useSelector((state) => findUserProfile(id)(state, userId));

    return (
        <FormPage title={t(TextTranslationKeys.userProfileView.edit.pageTitle)}>
            <UserProfileFormComponent
                userProfile={userProfile}
                handleSave={onSave}
            />
        </FormPage>
    );
}
