import React from 'react';
import {
    Switch, Route, useRouteMatch,
} from 'react-router';
import {
    TemplatesSelector,
    TemplatesEdit,
    TemplatesPreview,
} from '.';
import { TemplateCreateFromSlug } from './TemplatesCreate';
import TemplatesIndex from './TemplatesIndex/TemplatesIndex';

export default function TemplatesView() {
    const { path } = useRouteMatch();

    return (
        <Switch>
            <Route
                path={`${path}/:id/edit`}
                component={TemplatesEdit}
            />
            <Route
                path={`${path}/:id/preview`}
                component={TemplatesPreview}
            />
            <Route
                path={`${path}/new`}
                component={TemplatesSelector}
            />
            <Route
                path={`${path}/from/:designSourceSlug`}
                component={TemplateCreateFromSlug}
            />
            <Route
                path={`${path}/`}
                component={TemplatesIndex}
            />
        </Switch>
    );
}
