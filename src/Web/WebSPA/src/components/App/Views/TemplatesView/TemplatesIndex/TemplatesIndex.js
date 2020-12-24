import React from 'react';
import { useSelector } from 'react-redux';
import { findUserNewsletterTemplates } from 'redux/redux-orm/ORM';
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
            >
                { collection && collection.map(({
                    id, name,
                }) => (
                    <NewsletterListRowComponent
                        key={id}
                        id={id}
                        name={name}
                    />
                ))}
            </Table>
        </ListView>
    );
}

export default function TemplatesIndex() {
    const { accessToken, userId } = useUser();

    const newsletterTemplates = useSelector((state) => findUserNewsletterTemplates(state, userId));

    const hasTemplates = newsletterTemplates.length > 0;

    useContentApi(
        () => fetchNewsletters(accessToken, userId),
        accessToken,
    );

    return hasTemplates
        ? <NewsletterIndexList collection={newsletterTemplates} />
        : <TemplatesIndexIntro />;
}
