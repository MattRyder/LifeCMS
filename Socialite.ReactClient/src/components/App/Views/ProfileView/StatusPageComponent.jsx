import React from 'react';
import StatusContainer from '../../../Statuses/StatusContainer';
import CreateStatusComponent from '../../../Statuses/CreateStatus/CreateStatusComponent';

import './StatusPageComponent.scss';

export default function StatusPageComponent({ status }) {
    const statuses = status.statuses.sort((x, y) => x.createdAt < y.createdAt);

    return (
        <div className="status-page-component">
            <CreateStatusComponent />
            <StatusContainer
                statuses={statuses}
                loading={status.loading}
                error={status.error}
            />
        </div>
    )
};