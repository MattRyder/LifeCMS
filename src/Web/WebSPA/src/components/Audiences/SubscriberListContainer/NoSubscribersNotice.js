import React from 'react';
import { css } from 'emotion';
import { useTranslations } from 'hooks';

const styles = {
    notice: css`
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        padding: 2rem 1rem;
    `,
};

export default function NoSubscribersNotice() {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <div className={styles.notice}>
            <h4>{t(TextTranslationKeys.audienceView.details.noSubscribersNotice.title)}</h4>
            <p>{t(TextTranslationKeys.audienceView.details.noSubscribersNotice.cta)}</p>
        </div>
    );
}
