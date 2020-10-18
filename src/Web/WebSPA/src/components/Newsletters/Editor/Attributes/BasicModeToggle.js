import React from 'react';
import { cx, css } from 'emotion';
import PropTypes from 'prop-types';
import Toggle from 'react-toggle';
import { useTranslations } from 'hooks';

const style = {
    mode: css`
        align-items: center;
        border-bottom: 1px solid rgba(0, 0, 0, 0.15);
        display: flex;
        justify-content: space-between;
        font-size: 0.7rem;
        padding: 0.5rem;
    `,
};

function BasicModeToggle({ isBasicMode, handleToggle }) {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <span className={cx(style.mode)}>
            {isBasicMode
                ? t(TextTranslationKeys.newsletterView.editor.mode.basic)
                : t(TextTranslationKeys.newsletterView.editor.mode.advanced)}
            <Toggle
                icons={false}
                defaultChecked={isBasicMode}
                onChange={handleToggle}
            />
        </span>
    );
}

BasicModeToggle.propTypes = {
    isBasicMode: PropTypes.bool.isRequired,
    handleToggle: PropTypes.func.isRequired,
};

export default BasicModeToggle;
