import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCommentAlt, faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';
import { withTranslation } from 'react-i18next';
import TextTranslationKeys from '../../i18n/TextTranslationKeys';
import Status from './Status';
import StatusComponent from './StatusComponent';
import CenteredMessageComponent from '../App/Components/CenteredMessageComponent';

import './StatusContainer.scss';

class StatusContainer extends Component {
    renderStatuses() {
        const { statuses } = this.props;

        return statuses.map(({
            id, mood, text, createdAt,
        }) => <StatusComponent mood={mood} text={text} createdAt={createdAt} key={id} />);
    }

    renderNoneAvailable() {
        const { t } = this.props;

        return (
            <div className="none-available">
                <CenteredMessageComponent
                    message={t(TextTranslationKeys.status.noStatusesAvailable)}
                    icon={<FontAwesomeIcon icon={faCommentAlt} />}
                />
            </div>
        );
    }

    renderError() {
        const { error } = this.props;

        return (
            <div className="centered-message">
                <FontAwesomeIcon icon={faExclamationTriangle} />
                <p>{error.message}</p>
            </div>
        );
    }

    render() {
        const { error, statuses } = this.props;

        let renderable = null;

        if (error !== null) {
            renderable = this.renderError();
        } else if (statuses.length > 0) {
            renderable = this.renderStatuses();
        } else {
            renderable = this.renderNoneAvailable();
        }

        return (
            <div className="status-container">
                {renderable}
            </div>
        );
    }
}

StatusContainer.propTypes = {
    statuses: PropTypes.arrayOf(PropTypes.instanceOf(Status)),
    error: PropTypes.string,
};

StatusContainer.defaultProps = {
    statuses: [],
    error: '',
};

export default withTranslation()(StatusContainer);
