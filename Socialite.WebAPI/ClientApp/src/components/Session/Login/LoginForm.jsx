import React, { Component } from 'react';
import { connect } from 'react-redux';
import {
    Form, FormGroup, Button, Nav, NavLink, NavItem,
} from 'reactstrap';
import { Formik, Field } from 'formik';
import ReactstrapInput from '../../Util/Form';
import { getRedirectUriFromProps } from '../QueryString';
import Schema from './LoginSchema';
import { performLogin } from '../../../redux/actions/LoginActions';

import '../Session.scss';

const mapDispatchToProps = (dispatch) => ({
    performLogin: (loginParams) => dispatch(performLogin(loginParams)),
});

class LoginFormComponent extends Component {
    constructor(props) {
        super(props);

        this.state = {
            returnUrl: ''
        }
    }

    applyRedirectUri() {
        const returnUrl = getRedirectUriFromProps(this.props);

        this.setState({
            returnUrl,
        });
    }

    componentDidMount() {
        this.applyRedirectUri();
    }

    render() {
        const { returnUrl } = this.state;

        const { performLogin } = this.props;

        return (
            <div className="session-form">
                <div className="session-form-title">
                    <span className="session-form-text">Login to Socialite</span>
                </div>
                <Formik
                    initialValues={{
                        emailAddress: '',
                        password: '',
                    }}
                    validationSchema={Schema}
                    onSubmit={(values) => {
                        performLogin({
                            Email: values.emailAddress,
                            Password: values.password,
                        });
                    }}
                >
                    {({ handleSubmit, isSubmitting }) => (
                        <Form onSubmit={handleSubmit}>
                            <FormGroup htmlFor="input-email-address">
                                <Field
                                    type="email"
                                    name="emailAddress"
                                    id="input-email-address"
                                    placeholder="Email Address"
                                    component={ReactstrapInput}
                                />
                            </FormGroup>

                            <FormGroup htmlFor="input-password" style={{ marginTop: "2em" }}>
                                <Field
                                    type="password"
                                    name="password"
                                    id="input-password"
                                    placeholder="Password"
                                    component={ReactstrapInput}
                                />
                            </FormGroup>

                            <Button
                                variant="primary"
                                className="btn-submit"
                                type="submit"
                                block
                                disabled={isSubmitting}
                            >
                                Login
                    </Button>

                            <Nav fill>
                                <NavItem>
                                    <NavLink href="/accounts/forgot-details">Forgot details?</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink href={`/accounts/register?ReturnUrl=${returnUrl}`}>Sign up for Socialite</NavLink>
                                </NavItem>
                            </Nav>
                        </Form>
                    )}
                </Formik>
            </div >
        );
    }
}

export default connect(null, mapDispatchToProps)(LoginFormComponent);