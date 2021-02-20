import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import fileSize from 'filesize';
import { useTranslations } from 'hooks';
import useImageState from 'hooks/useImageState';
import ImageSelector from 'components/Util/Inputs/ImageSelector';
import { ImageIcon } from '../Toolbox/Icons';
import AttributePanelContainer from '../Components/Interface/AttributePanelContainer';

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

const PreviewContainer = ({ name, size, src }) => {
    const { t, TextTranslationKeys } = useTranslations();

    const {
        noImageSelected,
    } = TextTranslationKeys.newsletterView.editor.imageAttribute;

    const fileData = {
        name: name || t(noImageSelected),
        src: src || ImageIcon,
        size: fileSize(size),
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
                <p className={cx(size > 0 && styles.sizeNoFileSelected)}>{fileData.size}</p>
            </div>
        </div>
    );
};

PreviewContainer.propTypes = {
    name: PropTypes.string,
    size: PropTypes.oneOf([PropTypes.string, PropTypes.number]),
    src: PropTypes.string,
};

PreviewContainer.defaultProps = {
    name: '',
    size: 0,
    src: undefined,
};

export default function ImageAttribute({
    previewImageUrl, handleFileChange, urn,
}) {
    const { t, TextTranslationKeys } = useTranslations();

    const image = useImageState({ urn });

    return (
        <AttributePanelContainer title="Image">
            <div className={cx(styles.controls)}>
                <ImageSelector
                    defaultImageUrl={previewImageUrl}
                    newImageUrl={image.uri}
                    modalTitle={t(
                        TextTranslationKeys
                            .newsletterView
                            .editor
                            .imageAttribute
                            .fileModalLabel,
                    )}
                    setNewImage={(newFile) => handleFileChange(newFile)}
                />
            </div>
        </AttributePanelContainer>
    );
}

ImageAttribute.propTypes = {
    urn: PropTypes.string,
    previewImageUrl: PropTypes.string,
    handleFileChange: PropTypes.func.isRequired,
};

ImageAttribute.defaultProps = {
    urn: null,
    previewImageUrl: null,
};
