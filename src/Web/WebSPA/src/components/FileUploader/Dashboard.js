/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import { useDropzone } from 'react-dropzone';

const styles = {
    baseStyle: css({
        flex: 1,
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        borderWidth: 2,
        borderRadius: 5,
        borderColor: '#ddd',
        borderStyle: 'dashed',
        backgroundColor: '#fafafa',
        color: '#4c4c4c',
        outline: 'none',
        transition: 'border .24s ease-in-out',
        margin: '0.75rem',
        fontSize: '1.125rem',
        cursor: 'pointer',
        padding: '1rem',
    }),
    text: css`
        margin-bottom: 0;
    `,
};

export default function Dashboard({
    acceptedTypes, setAcceptedFiles, maxFiles,
}) {
    const { getRootProps, getInputProps } = useDropzone({
        maxFiles,
        accept: acceptedTypes,
        onDropAccepted: setAcceptedFiles,
    });

    return (
        <div {...getRootProps({ className: cx(styles.baseStyle) })}>
            <input {...getInputProps()} />

            <p className={cx(styles.text)}>
                Drop files here, or click to select files.
            </p>
        </div>
    );
}

Dashboard.propTypes = {
    acceptedTypes: PropTypes.string.isRequired,
    setAcceptedFiles: PropTypes.func.isRequired,
    maxFiles: PropTypes.number.isRequired,
};
