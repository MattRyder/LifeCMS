import React from 'react';
import { useParams } from 'react-router';
import SanitizedHTML from 'react-sanitized-html';
import {
    useContentApi, useUser, useStateSelector, useTranslations,
} from '../../../../hooks';
import { fetchNewsletters } from '../../../../redux/actions/NewsletterTemplateActions';

import './NewsletterTemplatePreview.scss';

export default function NewsletterTemplatePreview() {
    const { id } = useParams();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const newsletter = useStateSelector(userId, 'newsletter', 'newsletters', id);

    useContentApi(() => fetchNewsletters(accessToken, userId), accessToken, userId);

    return (
        <div className="newsletter-template-preview">
            <div className="title">
                <h2>{`${t(TextTranslationKeys.newsletterView.previewPageTitle)}: ${newsletter && newsletter.name}`}</h2>
            </div>
            <div className="page">
                <SanitizedHTML
                    html={newsletter && newsletter.html}
                    allowedAttributes={{ div: ['style'] }}
                />
            </div>
        </div>
    );
}
