import React, { Component } from 'react';
import {
    Link,
} from 'react-router-dom';
import { getParamFromSearch } from '../../../QueryString';

import '../Session.scss';

class ErrorFormComponent extends Component {
    constructor(props) {
        super(props);

        this.state = {
            returnUrl: ''
        }
    }

    applyRedirectUri() {
        const returnUrl = getParamFromSearch(this.props, "redirectUrl");

        this.setState({
            returnUrl,
        });
    }

    componentDidMount() {
        this.applyRedirectUri();
    }

    render() {
        const { returnUrl } = this.state;

        return (
            <div className="session-form">
                <div className="session-form-title">
                    <span className="session-form-text">Authorization Error</span>
                </div>

                <p>Request Details</p>
                <ul>
                    
                </ul>

                <Link to={returnUrl}>Return to...</Link>
            </div >
        );
    }
}

export default ErrorFormComponent;