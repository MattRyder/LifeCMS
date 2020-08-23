import React from 'react';
import {
    useRouteMatch, Switch, Route, Redirect,
} from 'react-router';
import NewsletterTemplateIndex from './TemplateIndex/NewsletterTemplateIndex';

export default function NewslettersView() {
    const { path } = useRouteMatch();

    return (
        <div style={{ padding: '2rem', width: '100%' }}>
            <Switch>
                <Route exact path={path}>
                    <Redirect to={`${path}/templates`} />
                </Route>
                <Route path={`${path}/templates`} component={NewsletterTemplateIndex} />
                <Route path={`${path}/mailing-lists`} />
                <Route path={`${path}/mailings`} />
            </Switch>
        </div>
    );
}
