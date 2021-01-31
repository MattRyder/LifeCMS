import React from 'react';
import ImageSelector from './ImageSelector';
import { useTranslations } from '../../../hooks';

export default function ImageFields({
    formik, avatarState, setNewAvatar, headerState, setNewHeader,
}) {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <>
            <ImageSelector
                imageState={avatarState}
                label={t(TextTranslationKeys.userProfile.properties.avatarImage)}
                modalTitle={t(TextTranslationKeys.userProfileView.create.avatarModalTitle)}
                setNewImage={(file) => setNewAvatar(file)}
                error={formik.errors.avatarImageUrn}
            />

            <ImageSelector
                imageState={headerState}
                label={t(TextTranslationKeys.userProfile.properties.headerImage)}
                modalTitle={t(TextTranslationKeys.userProfileView.create.headerModalTitle)}
                setNewImage={(file) => setNewHeader(file)}
                error={formik.errors.headerImageUrn}
            />
        </>
    );
}
