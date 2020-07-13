import React from 'react';
import { useSelector } from 'react-redux';
import { Button } from 'reactstrap';
import { Link, useRouteMatch } from 'react-router-dom';
import { useContentApi, useUser } from '../../../../../hooks';
import { fetchNewsletters } from '../../../../../redux/actions/NewsletterActions';
import NewsletterListComponent from './NewsletterListComponent';
import PageTitleBar from '../../../Components/PageTitleBar/PageTitleBar';

export default function NewsletterIndex() {
    const { accessToken, userId } = useUser();

    const newsletterState = useSelector(
        (state) => state.newsletter[userId],
    );

    const { path } = useRouteMatch();

    useContentApi(() => fetchNewsletters(accessToken, userId), accessToken, userId);

    return (
        <div className="newsletter-index">
            <PageTitleBar>
                <span>Newsletters</span>
                <Button
                    color="primary"
                    outline
                    to={`${path}/new`}
                    tag={Link}
                >
                    Create Newsletter
                </Button>

            </PageTitleBar>

            <NewsletterListComponent newsletters={newsletterState && newsletterState.newsletters} />
        </div>
    );
}
