import React, { Component } from 'react';
import PropTypes from 'proptypes';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCommentAlt, faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';
import Status from './Status';
import StatusComponent from './StatusComponent';
import CenteredMessageComponent from '../App/Components/CenteredMessageComponent';

import './StatusContainer.scss';

class StatusContainer extends Component {
    renderStatuses() {
        return this.props.statuses.map((s, idx) => <StatusComponent status={s} key={idx} />);
    }

    renderNoneAvailable() {
        return <CenteredMessageComponent
                message={"No statuses are available that have been authored by this person."}
                icon={<FontAwesomeIcon icon={faCommentAlt} />}
            />
        ;
    }

    renderError() {
        return (
            <div className="centered-message">
                <FontAwesomeIcon icon={faExclamationTriangle} />
                <p>{this.props.error.message}</p>
            </div>
        );
    }

    render() {
        let renderable = null;

        if (this.props.error !== null) {
            renderable = this.renderError();
        } else {
            if (this.props.statuses.length > 0) {
                renderable = this.renderStatuses();
            } else {
                renderable = this.renderNoneAvailable();
            }
        }

        return (
            <div className="status-container">
                {renderable}
            </div>
        );
    }
}

StatusContainer.propTypes = {
    statuses: PropTypes.arrayOf(PropTypes.instanceOf(Status))
};

StatusContainer.defaultProps = {
    statuses: []
};

export default StatusContainer;