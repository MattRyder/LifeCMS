import React from 'react';
import { FormikInput } from 'components/Util/Form';
import { useTranslations } from '../../../hooks';

export default function TextFields({ formik }) {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <>
            <FormikInput
                formik={formik}
                inputName="name"
                label={t(TextTranslationKeys.userProfile.create.name.label)}
                hint={t(TextTranslationKeys.userProfile.create.name.hint)}
            />

            <FormikInput
                formik={formik}
                inputName="email"
                label={t(TextTranslationKeys.userProfile.create.emailAddress)}
            />

            <FormikInput
                formik={formik}
                inputName="bio"
                label={t(TextTranslationKeys.userProfile.create.bio)}
            />

            <FormikInput
                formik={formik}
                inputName="occupation"
                label={t(TextTranslationKeys.userProfile.create.occupation)}
            />

            <FormikInput
                formik={formik}
                inputName="location"
                label={t(TextTranslationKeys.userProfile.create.location)}
            />
        </>
    );
}
