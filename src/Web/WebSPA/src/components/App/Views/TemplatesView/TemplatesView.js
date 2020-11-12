import React from 'react';
import { Switch, Route, useRouteMatch } from 'react-router';
import {
    TemplatesCreate,
    TemplatesEdit,
    TemplatesPreview,
} from '.';
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
                component={TemplatesCreate}
            />
            <Route
                path={`${path}/`}
                component={TemplatesIndex}
            />
        </Switch>
    );
}
