import React from 'react';
import PropTypes from 'prop-types';
import { css, cx } from 'emotion';
import { Modal, ModalBody, ModalHeader } from 'reactstrap';
import Dashboard from 'components/FileUploader/Dashboard';

const styles = {
    modalBody: css`
        padding: 0 !important;
    `,
};

export default function FileModal({
    acceptedTypes,
    isOpen,
    maxFiles,
    setAcceptedFiles,
    title,
    toggleIsOpen,
}) {
    const addUrlToFile = (file) => {
        const f = file;
        f.url = URL.createObjectURL(file);
        return f;
    };

    return (
        <Modal size="lg" isOpen={isOpen} toggle={toggleIsOpen}>
            <ModalHeader toggle={toggleIsOpen}>{title}</ModalHeader>
            <ModalBody className={cx(styles.modalBody)}>
                <Dashboard
                    acceptedTypes={acceptedTypes}
                    setAcceptedFiles={(files) => {
                        const filesWithUrl = files.map((file) => addUrlToFile(file));

                        setAcceptedFiles(filesWithUrl);

                        toggleIsOpen();
                    }}
                    maxFiles={maxFiles}
                />
            </ModalBody>
        </Modal>
    );
}

FileModal.propTypes = {
    acceptedTypes: PropTypes.string.isRequired,
    isOpen: PropTypes.bool.isRequired,
    maxFiles: PropTypes.number.isRequired,
    setAcceptedFiles: PropTypes.func.isRequired,
    title: PropTypes.string.isRequired,
    toggleIsOpen: PropTypes.func.isRequired,
};
