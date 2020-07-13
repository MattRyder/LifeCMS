import React from 'react';
import { Switch, Route, useRouteMatch } from 'react-router';
import NewsletterCreate from './NewsletterCreate/NewsletterCreate';
import NewsletterIndex from './NewsletterIndex/NewsletterIndex';
import NewsletterEdit from './NewsletterEdit/NewsletterEdit';

import './NewsletterView.scss';

export default function NewsletterView() {
    const { path } = useRouteMatch();

    return (
        <div className="newsletter-view">
            <Switch>
                <Route path={`${path}/new`} component={NewsletterCreate} />
                <Route path={`${path}/:id/edit`} component={NewsletterEdit} />
                <Route path={path} component={NewsletterIndex} />
            </Switch>
        </div>
    );
}
