import React from 'react';
import {
    useRouteMatch, Switch, Route,
} from 'react-router';
import CampaignIndex from './CampaignIndex/CampaignIndex';
import CampaignCreate from './CampaignCreate/CampaignCreate';
import CampaignUpdateSubject from './CampaignUpdateSubject/CampaignUpdateSubject';
import CampaignUpdateName from './CampaignUpdateName/CampaignUpdateName';
import CampaignDetails from './CampaignDetails/CampaignDetails';

import './CampaignView.scss';

export default function CampaignView() {
    const { path } = useRouteMatch();

    return (
        <div className="campaign-view">
            <Switch>
                <Route exact path={`${path}/:id/details`} component={CampaignDetails} />
                <Route path={`${path}/:id/update-name`} component={CampaignUpdateName} />
                <Route path={`${path}/:id/update-subject`} component={CampaignUpdateSubject} />
                <Route path={`${path}/new`} component={CampaignCreate} />
                <Route path={`${path}/`} component={CampaignIndex} />
            </Switch>
        </div>
    );
}
