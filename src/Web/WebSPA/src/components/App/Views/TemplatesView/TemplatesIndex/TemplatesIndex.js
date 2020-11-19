import React from 'react';
import { useSelector } from 'react-redux';
import { useContentApi, useUser, useTranslations } from '../../../../../hooks';
import { fetchNewsletters } from '../../../../../redux/actions/NewsletterTemplateActions';
import Table from '../../../../Util/Table/Table';
import NewsletterListRowComponent from './NewsletterListRowComponent';
import TemplatesIndexIntro from './TemplatesIndexIntro';
import ListView from '../../ListView';

function NewsletterIndexList({ collection }) {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <ListView
            title={t(TextTranslationKeys.newsletterView.dashboard.menu.templates)}
            ctaText={t(TextTranslationKeys.newsletterView.createNewsletter)}
            ctaLinkTo="/templates/new"
        >
            <Table
                headings={[
                    t(TextTranslationKeys.template.properties.name),
                ]}
                rowComponent={NewsletterListRowComponent}
                collection={collection}
            />
        </ListView>
    );
}

export default function TemplatesIndex() {
    const { accessToken, userId } = useUser();

    const templatesState = useSelector(
        (state) => state.newsletter[userId],
    );

    const hasTemplates = templatesState
        && templatesState.newsletters
        && templatesState.newsletters.length > 0;

    useContentApi(
        () => fetchNewsletters(accessToken, userId),
        accessToken,
        userId,
    );

    return hasTemplates
        ? (
            <NewsletterIndexList
                collection={templatesState && templatesState.newsletters}
            />
        ) : <TemplatesIndexIntro />;
}
