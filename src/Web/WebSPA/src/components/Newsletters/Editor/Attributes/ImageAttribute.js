import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import { Button } from 'reactstrap';
import fileSize from 'filesize';
import { useToggle, useTranslations } from 'hooks';
import { ImageIcon } from '../Toolbox/Icons';
import AttributePanelContainer from '../Components/Interface/AttributePanelContainer';
import FileModal from '../Components/FileModal';

const styles = {
    controls: css`
        padding: 1rem;
    `,
    imageContainer: css`
        display: flex;
        flex: 0.2;
        align-items: center;
        padding: 0 0.5rem;
    `,
    image: css`
        width: 100%;
        height: auto;
    `,
    previewContainer: css`
        display: flex;        
    `,
    previewInfo: css`
        flex: 1;
        display: flex;
        flex-direction: column;
        align-items: end;
        min-width: 0;
        
        p {
            margin-bottom: 0;
            width: 100%;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }
    `,
    uploadButton: css`
        display: flex;
        justify-content: end;
        padding: 0.75rem 0;
    `,
    sizeNoFileSelected: css`
        color: transparent;
    `,
};

const PreviewContainer = ({ file }) => {
    const { t, TextTranslationKeys } = useTranslations();

    const {
        noImageSelected,
    } = TextTranslationKeys.newsletterView.editor.imageAttribute;

    const fileData = {
        name: file.name || t(noImageSelected),
        src: file.url || ImageIcon,
        size: fileSize(file.size ? file.size : 0),
    };

    return (
        <div className={cx(styles.previewContainer)}>
            <div className={cx(styles.imageContainer)}>
                <img
                    className={cx(styles.image)}
                    alt={fileData.name}
                    src={fileData.src}
                />
            </div>
            <div className={cx(styles.previewInfo)}>
                <p>{fileData.name}</p>
                <p className={cx(!file.size && styles.sizeNoFileSelected)}>{fileData.size}</p>
            </div>
        </div>
    );
};

export default function ImageAttribute({ file, handleChange }) {
    const [isModalOpen, toggleModalOpen] = useToggle();

    const { t, TextTranslationKeys } = useTranslations();

    return (
        <AttributePanelContainer title="Image">
            <div className={cx(styles.controls)}>
                <PreviewContainer file={file} />

                <div className={cx(styles.uploadButton)}>
                    { file && (
                        <Button
                            color="link"
                            className="text-danger"
                            onClick={() => handleChange(undefined)}
                        >
                            {t(TextTranslationKeys.common.clear)}
                        </Button>
                    )}
                    <Button
                        color="primary"
                        onClick={toggleModalOpen}
                    >
                        {t(TextTranslationKeys.common.upload)}
                    </Button>
                </div>

                <FileModal
                    acceptedTypes="image/*"
                    isOpen={isModalOpen}
                    maxFiles={1}
                    setAcceptedFiles={(files) => handleChange(files[0])}
                    title="Choose an image"
                    toggleIsOpen={toggleModalOpen}
                />
            </div>
        </AttributePanelContainer>
    );
}

ImageAttribute.propTypes = {
    file: PropTypes.shape({
        url: PropTypes.string,
        name: PropTypes.string,
        size: PropTypes.number,
    }),
    handleChange: PropTypes.func.isRequired,
};

ImageAttribute.defaultProps = {
    file: {},
};
