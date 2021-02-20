import React from 'react';
import PropTypes from 'prop-types';
import ImageSelector from '../../Util/Inputs/ImageSelector';
import { useTranslations } from '../../../hooks';

export default function ImageFields({
    formik,
    existingAvatarUrl,
    newAvatarUrl,
    setNewAvatarFile,
    existingHeaderUrl,
    newHeaderUrl,
    setNewHeaderFile,
}) {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <>
            <ImageSelector
                defaultImageUrl={existingAvatarUrl}
                newImageUrl={newAvatarUrl}
                label={t(TextTranslationKeys.userProfile.properties.avatarImage)}
                modalTitle={t(TextTranslationKeys.userProfileView.create.avatarModalTitle)}
                setNewImage={(file) => setNewAvatarFile(file)}
                error={formik.errors.avatarImageUrn}
            />

            <ImageSelector
                defaultImageUrl={existingHeaderUrl}
                newImageUrl={newHeaderUrl}
                label={t(TextTranslationKeys.userProfile.properties.headerImage)}
                modalTitle={t(TextTranslationKeys.userProfileView.create.headerModalTitle)}
                setNewImage={(file) => setNewHeaderFile(file)}
                error={formik.errors.headerImageUrn}
            />
        </>
    );
}

ImageFields.propTypes = {
    formik: PropTypes.shape({
        errors: PropTypes.shape({
            avatarImageUrn: PropTypes.string,
            headerImageUrn: PropTypes.string,
        }),
    }),
    existingAvatarUrl: PropTypes.string,
    newAvatarUrl: PropTypes.string,
    setNewAvatarFile: PropTypes.func.isRequired,
    existingHeaderUrl: PropTypes.string,
    newHeaderUrl: PropTypes.string,
    setNewHeaderFile: PropTypes.func.isRequired,
};

ImageFields.defaultProps = {
    formik: {
        errors: {
            avatarImageUrn: undefined,
            headerImageUrn: undefined,
        },
    },
    existingAvatarUrl: '',
    newAvatarUrl: '',
    existingHeaderUrl: '',
    newHeaderUrl: '',
};
