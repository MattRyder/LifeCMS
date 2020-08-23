import React from 'react';
import { useSelector } from 'react-redux';
import { Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import { useContentApi, useUser, useTranslations } from '../../../../../hooks';
import { fetchNewsletters } from '../../../../../redux/actions/NewsletterTemplateActions';
import TableComponent from '../../../../Util/Table/TableComponent';
import NewsletterListRowComponent from './NewsletterListRowComponent';

import './NewsletterTemplateIndex.scss';

export default function NewsletterIndex() {
    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const newsletterState = useSelector(
        (state) => state.newsletter[userId],
    );

    useContentApi(() => fetchNewsletters(accessToken, userId), accessToken, userId);

    return (
        <div className="newsletter-template-index">
            <div className="title">
                <h2>{t(TextTranslationKeys.newsletterView.dashboard.menu.templates)}</h2>
                <Button tag={Link} size="sm" color="primary" to="/templates/new">
                    {t(TextTranslationKeys.newsletterView.createNewsletter)}
                </Button>
            </div>
            <TableComponent
                headings={['Name']}
                rowComponent={NewsletterListRowComponent}
                collection={newsletterState && newsletterState.newsletters}
            />
        </div>
    );
}
