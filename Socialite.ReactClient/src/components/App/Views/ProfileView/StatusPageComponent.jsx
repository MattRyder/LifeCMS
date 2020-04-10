import React from 'react';
import StatusContainer from '../../../Statuses/StatusContainer';
import CreateStatusComponent from '../../../Statuses/CreateStatus/CreateStatusComponent';

import './StatusPageComponent.scss';

export default function StatusPageComponent({
    accessToken,
    error,
    loading,
    statuses,
}) {
    const sortedStatuses = statuses ? statuses.sort((x, y) => x.createdAt < y.createdAt) : [];

    return (
        <div className="status-page-component">
            {accessToken ? <CreateStatusComponent accessToken={accessToken} /> : null}

            <StatusContainer
                statuses={sortedStatuses}
                loading={loading}
                error={error}
            />
        </div>
    )
};