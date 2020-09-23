import React from 'react';
import useTranslations from 'hooks/useTranslations';

import '../Session.scss';
import { useSelector } from 'react-redux';

export default function ErrorFormComponent() {
    const { t, TextTranslationKeys } = useTranslations();

    const { error } = useSelector((state) => state.errorState);

    return (
        <div className="session-form">
            <div className="session-form-title">
                <span className="session-form-text">{t(TextTranslationKeys.error.notice)}</span>
            </div>

            <strong>{t(TextTranslationKeys.error.requestDetails)}</strong>

            <p>
                {error}
            </p>
        </div>
    );
}
