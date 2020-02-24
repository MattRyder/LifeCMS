import React, { Component } from 'react';
import { Button } from 'reactstrap';
import { getParamFromSearch } from '../../../QueryString';

import '../Session.scss';

class LogoutComponent extends Component {
    constructor(props) {
        super(props);

        this.state = {
            returnUrl: "",
        };
    }

    componentDidMount() {
        const logoutId = getParamFromSearch(this.props, 'logoutId');

        this.setState({ logoutId });
    }

    render() {
        const { logoutId } = this.state;

        return (
            <div className="session-form">
                <div className="logout">
                    <h2>All signed out.</h2>
                    <p>
                        To navigate to Socialite,
                        <a href="/">click here.</a>
                    </p>
                </div>
            </div>
        );
    }
}

export default LogoutComponent;
