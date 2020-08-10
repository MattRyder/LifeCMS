import React from 'react';
import { Link, useRouteMatch } from 'react-router-dom';
import { Button, Table } from 'reactstrap';
import { useTranslations } from '../../../../../hooks';
import NewsletterListRowComponent from './NewsletterListRowComponent';

import './NewsletterListComponent.scss';

function NewsletterListComponent({ newsletters }) {
    const { path } = useRouteMatch();

    const { t, TextTranslationKeys } = useTranslations();

    return (
        <div className="newsletter-list-component">
            <Table>
                <caption className="a11y-visually-hidden">{t(TextTranslationKeys.newsletterView.listCaption)}</caption>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th label="actions">
                            <Button
                                color="primary"
                                outline
                                to={`${path}/new`}
                                tag={Link}
                            >
                                {t(TextTranslationKeys.newsletterView.createNewsletter)}
                            </Button>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        newsletters.length > 0
                            ? newsletters.map((newsletter) => (
                                <NewsletterListRowComponent newsletter={newsletter} />
                            )) : null
                    }
                </tbody>
            </Table>
        </div>
    );
}

NewsletterListComponent.defaultProps = {
    newsletters: [],
};

export default NewsletterListComponent;
