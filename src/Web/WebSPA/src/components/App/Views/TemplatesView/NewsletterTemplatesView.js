import React from 'react';
import { Switch, Route, useRouteMatch } from 'react-router';
import {
    NewsletterTemplateCreate, NewsletterTemplateEdit, NewsletterTemplatePreview,
} from '.';

import './NewsletterTemplatesView.scss';

export default function NewsletterTemplatesView() {
    const { path } = useRouteMatch();

    return (
        <div className="newsletter-templates-view">
            <Switch>
                <Route path={`${path}/new`} component={NewsletterTemplateCreate} />
                <Route path={`${path}/:id/edit`} component={NewsletterTemplateEdit} />
                <Route path={`${path}/:id/preview`} component={NewsletterTemplatePreview} />
            </Switch>
        </div>
    );
}
