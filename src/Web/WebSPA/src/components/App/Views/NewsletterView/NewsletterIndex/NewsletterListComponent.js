import React from 'react';
import { Link, useRouteMatch } from 'react-router-dom';
import { Table } from 'reactstrap';
import { useTranslations } from '../../../../../hooks';
import Icon, { Icons } from '../../../Iconography/Icon';
import NewsletterListRowComponent from './NewsletterListRowComponent';

import './NewsletterListComponent.scss';

function NewsletterListComponent({ newsletters }) {
    const { path } = useRouteMatch();

    const { t, TextTranslationKeys } = useTranslations();

    return (
        <div className="newsletter-list-component">
            <Table>
                <caption className="a11y-visually-hidden">
                    {t(TextTranslationKeys.newsletterView.listCaption)}
                </caption>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th label="actions">
                            <Link
                                color="link"
                                to={`${path}/new`}
                            >
                                <span><Icon icon={Icons.plus} /></span>
                                {t(TextTranslationKeys.newsletterView.createNewsletter)}
                            </Link>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        newsletters.length > 0
                            ? newsletters.map((newsletter) => (
                                <NewsletterListRowComponent key={newsletter.id} newsletter={newsletter} />
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
