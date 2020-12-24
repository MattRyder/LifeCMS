import React from 'react';
import {
    useRouteMatch, Switch, Route,
} from 'react-router';
import AudienceAddSubscribers from './AudienceAddSubscribers/AudienceAddSubscribers';
import AudienceCreate from './AudienceCreate/AudienceCreate';
import AudienceDetails from './AudienceDetails/AudienceDetails';
import AudienceIndex from './AudienceIndex/AudienceIndex';
import AudienceUpdateName from './AudienceUpdateName/AudienceUpdateName';

export default function AudienceView() {
    const { path } = useRouteMatch();

    return (
        <Switch>
            <Route exact path={`${path}/:id/add-subscribers`} component={AudienceAddSubscribers} />
            <Route exact path={`${path}/:id/update-name`} component={AudienceUpdateName} />
            <Route exact path={`${path}/new`} component={AudienceCreate} />
            <Route exact path={`${path}/:id`} component={AudienceDetails} />
            <Route path={`${path}/`} component={AudienceIndex} />
        </Switch>
    );
}
