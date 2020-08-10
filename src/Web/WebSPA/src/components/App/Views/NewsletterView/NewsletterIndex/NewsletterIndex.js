import React from 'react';
import { useSelector } from 'react-redux';
import { useContentApi, useUser, useTranslations } from '../../../../../hooks';
import { fetchNewsletters } from '../../../../../redux/actions/NewsletterActions';
import NewsletterListComponent from './NewsletterListComponent';
import PageTitleBar from '../../../Components/PageTitleBar/PageTitleBar';

export default function NewsletterIndex() {
    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const newsletterState = useSelector(
        (state) => state.newsletter[userId],
    );

    useContentApi(() => fetchNewsletters(accessToken, userId), accessToken, userId);

    return (
        <div className="newsletter-index">
            <PageTitleBar>
                <span>{t(TextTranslationKeys.newsletterView.pageTitle)}</span>
            </PageTitleBar>

            <NewsletterListComponent newsletters={newsletterState && newsletterState.newsletters} />
        </div>
    );
}
