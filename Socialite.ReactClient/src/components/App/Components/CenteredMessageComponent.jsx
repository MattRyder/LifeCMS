import React from 'react';

import './CenteredMessageComponent.scss';

export default function CenteredMessageComponent(props) {
    const { icon, message } = props;

    return (
        <div className="centered-message">
            {icon}
            <p>{message}</p>
        </div>
    );
}