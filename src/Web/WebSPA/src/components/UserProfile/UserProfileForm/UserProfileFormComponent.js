import React from 'react';
import { useFormik } from 'formik';
import {
    Input, Button, FormFeedback, Label,
} from 'reactstrap';
import { useDispatch } from 'react-redux';
import Schema, { InitialValues } from './UserProfileFormSchema';
import { useTranslations, useUser } from '../../../hooks';
import { createUserProfile } from '../../../redux/actions/UserProfileActions';

import './UserProfileFormComponent.scss';

const getInputFor = (formik, inputName, label, hint) => (
    <div className={`input-${inputName}`}>
        <Label for={inputName}>{label}</Label>
        <Input
            name={inputName}
            onChange={formik.handleChange}
            invalid={formik.errors[inputName] && formik.errors[inputName].length > 0}
            value={formik.values[inputName]}
        />
        <FormFeedback valid={formik.errors[inputName] && formik.errors[inputName].length === 0} />
        { hint ? <span className="hint">{hint}</span> : null }
    </div>
);

export default function UserProfileFormComponent() {
    const { t, TextTranslationKeys } = useTranslations();

    const { accessToken, userId } = useUser();

    const dispatch = useDispatch();

    const formik = useFormik({
        initialValues: InitialValues,
        validationSchema: Schema,
        onSubmit: (values) => {
            const params = {
                name: values.name,
                email_address: values.email,
                bio: values.bio,
                occupation: values.occupation,
                location: values.location,
                avatarImageUri: values.avatarImageUri,
                headerImageUri: values.headerImageUri,
            };

            dispatch(createUserProfile(accessToken, userId, params));
        },
    });

    return (
        <div className="user-profile-form-component">
            <form onSubmit={formik.handleSubmit}>
                { getInputFor(
                    formik,
                    'name',
                    t(TextTranslationKeys.userProfile.create.name.label),
                    t(TextTranslationKeys.userProfile.create.name.hint),
                ) }

                { getInputFor(
                    formik,
                    'email',
                    t(TextTranslationKeys.userProfile.create.emailAddress),
                ) }

                { getInputFor(
                    formik,
                    'bio',
                    t(TextTranslationKeys.userProfile.create.bio),
                ) }

                <div className="what-where">
                    { getInputFor(
                        formik,
                        'occupation',
                        t(TextTranslationKeys.userProfile.create.occupation),
                    ) }

                    { getInputFor(
                        formik,
                        'location',
                        t(TextTranslationKeys.userProfile.create.location),
                    ) }
                </div>

                { getInputFor(
                    formik,
                    'avatarImageUri',
                    t(TextTranslationKeys.userProfile.create.avatarImageUri),
                ) }

                { getInputFor(
                    formik,
                    'headerImageUri',
                    t(TextTranslationKeys.userProfile.create.headerImageUri),
                ) }

                <Button variant="default" type="submit" disabled={formik.isSubmitting}>
                    {t(TextTranslationKeys.common.save)}
                </Button>
            </form>
        </div>
    );
}
