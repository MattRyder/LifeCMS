import React, { useRef } from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import { useToggle, useTranslations } from 'hooks';
import Icon, { Icons } from 'components/App/Iconography/Icon';
import { Button } from 'reactstrap';
import theme from 'theme';

const styles = {
    editableText: css` 
        display: flex;
    `,
    label: css`
        display: flex;
        align-items: center;
        margin-right: 0.25rem;
    `,
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
    displayText: css`
        display: flex;
        align-items: center;
    `,
};

export default function EditableText({
    defaultIsEditable, label, text, handleTextChange, className,
}) {
    const [isEditable, toggleEditable] = useToggle(defaultIsEditable);

    const { t, TextTranslationKeys } = useTranslations();

    const inputRef = useRef(null);

    const toggleFocus = () => {
        toggleEditable();

        setTimeout(
            () => inputRef.current && inputRef.current.focus(),
            50,
        );
    };

    return (
        <div className={cx(styles.editableText, className)}>
            {label && <span className={styles.label}>{`${label}:`}</span>}
            {isEditable ? (
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
                <div className={styles.displayText}>
                    <button
                        type="button"
                        className={cx(styles.nameButton, (!text && styles.placeholder))}
                        onClick={toggleFocus}
                    >
                        {text || t(TextTranslationKeys.editableText.placeholder)}
                    </button>
                </div>
            )}
        </div>
    );
}

EditableText.propTypes = {
    className: PropTypes.string,
    defaultIsEditable: PropTypes.bool,
    label: PropTypes.string,
    text: PropTypes.string,
    handleTextChange: PropTypes.func.isRequired,
};

EditableText.defaultProps = {
    className: '',
    defaultIsEditable: false,
    label: '',
    text: '',
};
