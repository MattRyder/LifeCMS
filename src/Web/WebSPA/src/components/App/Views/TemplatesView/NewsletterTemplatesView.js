import React from 'react';
import { cx, css } from 'emotion';
import { Switch, Route, useRouteMatch } from 'react-router';
import {
    NewsletterTemplateCreate,
    NewsletterTemplateEdit,
    NewsletterTemplatePreview,
} from '.';

const style = css`
    height: 100%;
    width: 100%;
`;

export default function NewsletterTemplatesView() {
    const { path } = useRouteMatch();

    return (
        <div className={cx(style)}>
            <Switch>
                <Route
                    path={`${path}/new`}
                    component={NewsletterTemplateCreate}
                />
                <Route
                    path={`${path}/:id/edit`}
                    component={NewsletterTemplateEdit}
                />
                <Route
                    path={`${path}/:id/preview`}
                    component={NewsletterTemplatePreview}
                />
            </Switch>
        </div>
    );
}
