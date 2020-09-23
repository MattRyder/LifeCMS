import React from 'react';
import PropTypes from 'prop-types';
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
        text: 'info',
        icon: faInfoCircle,
    },
    success: {
        text: 'success',
        icon: faCheckCircle,
    },
    warn: {
        text: 'warn',
        icon: faExclamationCircle,
    },
    error: {
        text: 'error',
        icon: faTimesCircle,
    },
};

function MessageContainer({ type, title, messages = [] }) {
    return (
        <div className={`message-container message-container-${type.text}`}>
            <div className="icon">
                <FontAwesomeIcon icon={type.icon} size="lg" />
            </div>
            <div>
                <p className="title">{title}</p>
                <ul>
                    {messages.map((message, i) => <li key={i}>{message}</li>)}
                </ul>
            </div>
        </div>
    );
}

MessageContainer.propTypes = {
    type: PropTypes.shape({
        text: PropTypes.string.isRequired,
        icon: PropTypes.object.isRequired,
    }).isRequired,
    title: PropTypes.string.isRequired,
    messages: PropTypes.arrayOf(PropTypes.string),
};

MessageContainer.defaultProps = {
    messages: [],
};

export default MessageContainer;
