import React from 'react';
import PropTypes from 'prop-types';
import strftime from 'strftime';

import './StatusComponent.scss';

function StatusComponent({ mood, text, createdAt }) {
    return (
        <div className="status">
            <span className="mood">
                is feeling:
                {' '}
                {mood}
            </span>

            <div className="message">
                <p>{text}</p>
            </div>

            <span className="created-at" title={createdAt}>
                {strftime('%H:%M • %d %B %Y', new Date(createdAt))}
            </span>
        </div>
    );
}

StatusComponent.propTypes = {
    mood: PropTypes.string,
    text: PropTypes.string,
    createdAt: PropTypes.string,
};

StatusComponent.defaultProps = {
    mood: '',
    text: '',
    createdAt: '',
};

export default StatusComponent;
