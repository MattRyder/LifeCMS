import React, { useRef } from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import { useToggle, useTranslations } from 'hooks';
import Icon, { Icons } from 'components/App/Iconography/Icon';
import { Button } from 'reactstrap';
import theme from 'theme';

const styles = {
    nameButton: css`
        background-color: transparent;
        border: none;
        border-bottom: 2px dotted #117cfa;
        color: #117cfa;
        margin-left: 0.5rem;
        padding: 0;
    `,
    titleEdit: css`
        display: flex;
        justify-self: center;
        flex: 1;

        input {
            margin-right: 0.25rem;
        }
    `,
    placeholder: css`
        color: ${theme.colors.textPlaceholder};
        border-bottom: 2px dotted ${theme.colors.textPlaceholder};

    `,
};

export default function EditableText({ defaultIsEditable, text, handleTextChange }) {
    const [isEditable, toggleEditable] = useToggle(defaultIsEditable);

    const { t, TextTranslationKeys } = useTranslations();

    const inputRef = useRef(null);

    const toggleFocus = () => {
        toggleEditable();

        setTimeout(() => inputRef.current && inputRef.current.focus(), 50);
    };

    return (
        isEditable ? (
            <div className={cx(styles.titleEdit)}>
                <input
                    type="text"
                    ref={inputRef}
                    value={text}
                    onChange={(e) => {
                        handleTextChange(e.currentTarget.value);
                    }}
                />
                <Button
                    type="button"
                    color="primary"
                    size="sm"
                    onClick={toggleEditable}
                >
                    <Icon icon={Icons.check} />
                </Button>
            </div>
        ) : (
            <div>
                <button
                    type="button"
                    className={cx(styles.nameButton, (!text && styles.placeholder))}
                    onClick={toggleFocus}
                >
                    {text || t(TextTranslationKeys.editableText.placeholder)}
                </button>
            </div>
        )
    );
}

EditableText.propTypes = {
    defaultIsEditable: PropTypes.bool,
    text: PropTypes.string,
    handleTextChange: PropTypes.func.isRequired,
};

EditableText.defaultProps = {
    defaultIsEditable: false,
    text: '',
};
