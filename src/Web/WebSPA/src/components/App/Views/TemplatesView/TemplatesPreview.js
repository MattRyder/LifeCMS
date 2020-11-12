import React from 'react';
import { useParams } from 'react-router';
import SanitizedHTML from 'react-sanitized-html';
import { fetchNewsletters } from 'redux/actions/NewsletterTemplateActions';
import {
    useContentApi, useUser, useStateSelector, useTranslations,
} from 'hooks';
import FormPage from 'components/Util/FormPage';

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

    const title = `${
        t(TextTranslationKeys.newsletterView.previewPageTitle)
    }: ${newsletter && newsletter.name}`;

    return (
        <FormPage title={title}>
            <SanitizedHTML
                html={newsletter && newsletter.html}
                allowedAttributes={{ div: ['style'] }}
            />
        </FormPage>
    );
}
