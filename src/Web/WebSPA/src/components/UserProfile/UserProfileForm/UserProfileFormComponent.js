import React from 'react';
import PropTypes from 'prop-types';
import { useFormik } from 'formik';
import { Button } from 'reactstrap';
import { css, cx } from 'emotion';
import { v4 as uuidv4 } from 'uuid';
import { boxShadow } from 'theme';
import UploadFileService from 'services/UploadFileService';
import { useMobileViewport, useTranslations, useUser } from '../../../hooks';
import Schema, { InitialValues } from './UserProfileFormSchema';
import ImageFields from './ImageFields';
import TextFields from './TextFields';
import useImageState from './useImageState';

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
        flex: 1;
        justify-content: space-around;
        flex-direction: column;

        @media(min-width: 768px) {
            flex-direction: row;
        }
    `,
    desktop: css`
        display: flex;
        > div {
            flex: 1;
            padding: 2rem;
        }
    `,
};

function SaveButton({ disabled }) {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <Button
            variant="default"
            block
            type="submit"
            disabled={disabled}
        >
            {t(TextTranslationKeys.common.save)}
        </Button>
    );
}

SaveButton.propTypes = {
    disabled: PropTypes.bool.isRequired,
};

const MobileLayout = ({
    formik, avatarState, setNewAvatar, headerState, setNewHeader,
}) => (
    <div>
        <TextFields
            formik={formik}
        />

        <ImageFields
            formik={formik}
            avatarState={avatarState}
            setNewAvatar={setNewAvatar}
            headerState={headerState}
            setNewHeader={setNewHeader}
        />

        <SaveButton disabled={formik.isSubmitting} />
    </div>
);

const DesktopLayout = ({
    formik, avatarState, setNewAvatar, headerState, setNewHeader,
}) => (
    <div className={cx(styles.desktop)}>
        <div>
            <TextFields formik={formik} />

            <SaveButton disabled={formik.isSubmitting} />
        </div>
        <div>
            <ImageFields
                formik={formik}
                avatarState={avatarState}
                setNewAvatar={setNewAvatar}
                headerState={headerState}
                setNewHeader={setNewHeader}
            />
        </div>
    </div>
);

export default function UserProfileFormComponent({ userProfile, handleSave }) {
    const { accessToken } = useUser();

    const [avatarState, setNewAvatar] = useImageState({
        urn: userProfile.avatarImageUrn,
    });

    const [headerState, setNewHeader] = useImageState({
        urn: userProfile.headerImageUrn,
    });

    const isMobileOrTablet = useMobileViewport();

    const formik = useFormik({
        initialValues: userProfile,
        validationSchema: Schema,
        onSubmit: async (values) => {
            const imageUrn = avatarState.newFile && avatarState.newFile.url
                ? await UploadFileService(
                    accessToken,
                    uuidv4(),
                    avatarState.newFile.type,
                    avatarState.newFile.url,
                )
                : undefined;

            const headerUrn = headerState.newFile && headerState.newFile.url
                ? await UploadFileService(
                    accessToken,
                    uuidv4(),
                    headerState.newFile.type,
                    headerState.newFile.url,
                )
                : undefined;

            const params = {
                name: values.name,
                email_address: values.email,
                bio: values.bio,
                occupation: values.occupation,
                location: values.location,
                avatar_image_urn: imageUrn,
                header_image_urn: headerUrn,
            };

            handleSave(params);
        },
    });

    return (
        <div className={cx(styles.main)}>
            <form
                className={cx(styles.form)}
                onSubmit={formik.handleSubmit}
            >
                {isMobileOrTablet ? (
                    <MobileLayout
                        formik={formik}
                        avatarState={avatarState}
                        setNewAvatar={setNewAvatar}
                        headerState={headerState}
                        setNewHeader={setNewHeader}
                    />
                ) : (
                    <DesktopLayout
                        formik={formik}
                        avatarState={avatarState}
                        setNewAvatar={setNewAvatar}
                        headerState={headerState}
                        setNewHeader={setNewHeader}
                    />
                )}
            </form>
        </div>
    );
}

UserProfileFormComponent.propTypes = {
    userProfile: PropTypes.shape({
        avatarImageUrn: PropTypes.string,
        headerImageUrn: PropTypes.string,
    }),
    handleSave: PropTypes.func.isRequired,
};

UserProfileFormComponent.defaultProps = {
    userProfile: InitialValues,
};
