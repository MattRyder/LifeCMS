import React from 'react'
import PropTypes from 'proptypes';
import strftime from 'strftime';
import Status from './Status';

import './StatusComponent.scss';

class StatusComponent extends React.Component {
    render() {
        return (
            <div className="status">
                <span className="mood">is feeling: {this.props.status.mood}</span>

                <div className="message">
                    <p>{this.props.status.text}</p>
                </div>

                <span className="created-at" title={this.props.status.createdAt.toString()}>
                    {strftime("%H:%M • %d %B %Y", this.props.status.createdAt)}
                </span>
            </div>
        );
    }
}

StatusComponent.propTypes = {
    status: PropTypes.instanceOf(Status).isRequired
};

export default StatusComponent;