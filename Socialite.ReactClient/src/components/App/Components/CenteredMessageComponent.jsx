import React from 'react';

import './CenteredMessageComponent.scss';

export default function CenteredMessageComponent({ icon, message }) {
    return (
        <div className="centered-message">
            {icon}
            <p>{message}</p>
        </div>
    );
}