import React from 'react';
import { useFormik } from 'formik';
import { Button } from 'reactstrap';
import { useDispatch } from 'react-redux';
import getInputFor from 'components/Util/Form';
import { css, cx } from 'emotion';
import { boxShadow } from 'theme';
import { useTranslations, useUser } from '../../../hooks';
import { createUserProfile } from '../../../redux/actions/UserProfileActions';
import Schema, { InitialValues } from './UserProfileFormSchema';

const styles = {
    main: css`
        background-color: #fff;
        padding: 1rem;
        ${boxShadow('rgba(0, 0, 0, 0.05)')}

    `,
    hint: css`
        font-size: smaller;
        color: lighten(gray, 5%);
    `,
    form: css`
        display: flex;
        flex-direction: column;
        justify-content: space-around;

        > div {
            padding: 0.5rem 0;
        }
    `,
};

export default function UserProfileFormComponent({ userProfile = InitialValues }) {
    const { t, TextTranslationKeys } = useTranslations();

    const { accessToken, userId } = useUser();

    const dispatch = useDispatch();

    const formik = useFormik({
        initialValues: userProfile,
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

            dispatch(createUserProfile(accessToken, userId, params, '/user-profiles'));
        },
    });

    return (
        <div className={cx(styles.main)}>
            <form
                className={cx(styles.form)}
                onSubmit={formik.handleSubmit}
            >
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
