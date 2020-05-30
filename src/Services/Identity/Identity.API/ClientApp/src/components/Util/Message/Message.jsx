import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    faInfoCircle,
    faCheckCircle,
    faExclamationCircle,
    faTimesCircle,
} from '@fortawesome/free-solid-svg-icons';

import './Message.scss';

export const MessageType = {
    info: {
        text: "info",
        icon: faInfoCircle,
    },
    success: {
        text: "success",
        icon: faCheckCircle,
    },
    warn: {
        text: "warn",
        icon: faExclamationCircle,
    },
    error: {
        text: "error",
        icon: faTimesCircle,
    },
};

export const MessageContainer = ({ type, title, messages = [] }) => (
    <div className={`message-container message-container-${type.text}`}>
        <div className={`icon`}>
            <FontAwesomeIcon icon={type.icon} size="lg" />
        </div>
        <div>
            <p className="title">{title}</p>
            <ul>
                {messages.map((message) => <li>{message}</li>)}
            </ul>
        </div>
    </div >
);