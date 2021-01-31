import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import {
    useMobileViewport, useToggle, useTranslations,
} from 'hooks';
import { Button } from 'reactstrap';
import FileModal from 'components/Newsletters/Editor/Components/FileModal';
import theme from 'theme';
import Icon, { Icons } from 'components/App/Iconography/Icon';

const styles = {
    imageSelector: css`
        border: 1px solid rgba(0, 0, 0, 0.1);
        border-radius: 5px;
        display: flex;
        flex-direction: column;
        margin-bottom: 1rem;
        padding: 0.75rem;
    `,
    header: css`
        background-repeat: no-repeat;
        background-size: cover;
    `,
    controls: css`
        align-items: center;
        display: flex;
        flex: 1;
    `,
    buttons: css`
        display: flex;
        flex: 1;
        padding: 1rem;
        flex-direction: column;

        @media(min-width: 1199px) {
            flex-direction: row;
        }
    `,
    label: css`
        @media(max-width: 1199px) {
            margin: 0;
        }
    `,
    error: css`
        font-size: smaller;
    `,
    button: css`
        margin-right: 0.35rem;
    `,
    imageBox: css`
        width: 5rem;
        max-width: 5rem;
    `,
    image: css`
        width: 100%;
        min-width: 5rem;
        min-height: 5rem;
        height: auto;
        border: 1px solid rgba(0, 0, 0, 0.2);
    `,
    imagePresent: css`
        flex: 1;
    `,
    noImagePresent: css`
        background-color: ${theme.colors.tableHeaderBackground};
        border: 1px solid: ${theme.colors.tableBorder};
        width: 100%;
        height: 5rem;
        display: flex;
        align-items: center;
        justify-content: center;
        border-radius: 5px;
    `,
};

export default function ImageSelector({
    label, modalTitle, imageState, setNewImage, error,
}) {
    const [isModalOpen, toggleModal] = useToggle();

    const shownImage = imageState.newFile.url || imageState.uri;

    const imageStyle = shownImage
        ? styles.imagePresent
        : styles.noImagePresent;

    const isMobileOrTablet = useMobileViewport();

    const { t, TextTranslationKeys } = useTranslations();

    return (
        <div className={styles.imageSelector}>
            <p className={styles.label}>
                {label}
            </p>

            <div className={styles.controls}>
                <div className={cx(styles.imageBox, imageStyle)}>
                    {shownImage ? (
                        <img
                            className={styles.image}
                            src={shownImage}
                            alt={t(TextTranslationKeys.userProfile.properties.avatarImage)}
                        />
                    ) : (
                        <Icon icon={Icons.photo} />
                    )}
                </div>
                <div className={styles.buttons}>
                    <Button
                        outline
                        color="primary"
                        size="sm"
                        onClick={toggleModal}
                        className={styles.button}
                        block={isMobileOrTablet}
                    >
                        {t(TextTranslationKeys.common.upload)}
                    </Button>
                    <Button
                        outline
                        color="danger"
                        size="sm"
                        className={styles.button}
                        onClick={() => setNewImage({})}
                        block={isMobileOrTablet}
                    >
                        {t(TextTranslationKeys.common.clear)}
                    </Button>
                </div>
            </div>

            {error && <p className={cx('text-danger', styles.error)}>{error}</p>}

            <FileModal
                acceptedTypes="image/*"
                isOpen={isModalOpen}
                maxFiles={1}
                setAcceptedFiles={(files) => setNewImage(files[0])}
                title={modalTitle}
                toggleIsOpen={toggleModal}
            />
        </div>
    );
}

ImageSelector.propTypes = {
    label: PropTypes.string,
    modalTitle: PropTypes.string,
    error: PropTypes.string,
    imageState: PropTypes.shape({
        urn: PropTypes.string,
        uri: PropTypes.string,
        newFile: PropTypes.shape({
            url: PropTypes.string,
        }),
    }),
    setNewImage: PropTypes.func.isRequired,
};

ImageSelector.defaultProps = {
    label: '',
    modalTitle: '',
    error: undefined,
    imageState: {
        urn: null,
        uri: null,
        newFile: {
            url: null,
        },
    },
};
