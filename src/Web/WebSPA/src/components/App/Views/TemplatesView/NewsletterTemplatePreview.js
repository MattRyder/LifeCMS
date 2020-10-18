import React from 'react';
import { cx, css } from 'emotion';
import { useParams } from 'react-router';
import SanitizedHTML from 'react-sanitized-html';
import ViewNavigationBar from 'components/App/Components/ViewNavigationBar/ViewNavigationBar';
import { fetchNewsletters } from 'redux/actions/NewsletterTemplateActions';
import {
    useContentApi, useUser, useStateSelector, useTranslations,
} from 'hooks';

const styles = {
    main: css`
        display: flex;
        flex-direction: column;
        height: 100%;
        padding: 2rem;
    `,
    title: css`
        display: flex;
        justify-content: space-between;
        padding: 0.5rem 0;

        h2 {
            font-size: 1.25rem;
            margin-bottom: 0;
        }
    `,
    page: css`
        box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05),
            0 2px 2px rgba(0, 0, 0, 0.05),
            0 4px 4px rgba(0, 0, 0, 0.05),
            0 6px 8px rgba(0, 0, 0, 0.05),
            0 8px 16px rgba(0, 0, 0, 0.05);
        background-color: white;
        min-height: 50%;
        padding: 1rem;
    `,
};

export default function NewsletterTemplatePreview() {
    const { id } = useParams();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const newsletter = useStateSelector(
        userId,
        'newsletter',
        'newsletters',
        id,
    );

    useContentApi(
        () => fetchNewsletters(accessToken, userId),
        accessToken,
        userId,
    );

    return (
        <div className={cx(styles.main)}>
            <ViewNavigationBar showBackLink />

            <div className={cx(styles.title)}>
                <h2>
                    {`${t(TextTranslationKeys.newsletterView.previewPageTitle)}:
                    ${newsletter && newsletter.name}`}
                </h2>
            </div>
            <div className={cx(styles.page)}>
                <SanitizedHTML
                    html={newsletter && newsletter.html}
                    allowedAttributes={{ div: ['style'] }}
                />
            </div>
        </div>
    );
}
