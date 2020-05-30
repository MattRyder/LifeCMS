import React, { Component } from 'react';
import { Button } from 'reactstrap';
import Fade from 'react-reveal';
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
                    <h2>Signing out</h2>

                    <p>Are you sure you want to be signed out of LifeCMS?</p>

                    <form action="/api/v1/accounts/logout" method="post">
                        <input type="hidden" name="logoutId" value={logoutId} />

                        <Button color="primary">Yes, sign out now.</Button>
                    </form>
                </div>
            </div>
        );
    }
}

export default LogoutComponent;
