import React, { Component } from 'react';
import { connect } from 'react-redux';
import Fade from 'react-reveal';
import {
    Form, FormGroup, Button, Nav, NavLink, NavItem,
} from 'reactstrap';
import { Formik, Field } from 'formik';
import ReactstrapInput from '../../Util/Form';
import { createUrlWithQueryString, getParamFromSearch } from '../../../QueryString';
import Schema from './LoginSchema';
import { performAuthentication } from '../../../redux/actions/AuthenticationActions';
import { MessageContainer, MessageType } from '../../Util/Message/Message';

import '../Session.scss';

const mapStateToProps = ({ authenticationState }) => ({
    isLoading: authenticationState.isLoading,
    errors: authenticationState.errors,
});

const mapDispatchToProps = (dispatch) => ({
    dispatchAuthentication: (authenticationParams) => dispatch(performAuthentication(authenticationParams)),
});

class LoginFormComponent extends Component {
    constructor(props) {
        super(props);

        this.state = {
            returnUrl: '',
            registerUrl: '/accounts/register',
        }
    }

    applyRedirectUri() {
        const returnUrl = getParamFromSearch(this.props, "ReturnUrl");

        const registerUrl = createUrlWithQueryString('/accounts/register', {
            ReturnUrl: returnUrl,
        });

        this.setState({
            returnUrl,
            registerUrl,
        });
    }

    componentDidMount() {
        this.applyRedirectUri();
    }

    render() {
        const { returnUrl, registerUrl } = this.state;

        const { dispatchAuthentication, errors = [] } = this.props;

        return (
            <div>
                <Fade bottom when={errors.length > 0}>
                    <MessageContainer type={MessageType.error} title="Error" messages={errors} />
                </Fade>
                <div className="session-form">
                    <div className="session-form-title">
                        <span className="session-form-text">Login to LifeCMS</span>
                    </div>
                    <Formik
                        initialValues={{
                            emailAddress: '',
                            password: '',
                        }}
                        validationSchema={Schema}
                        onSubmit={(values) => {
                            dispatchAuthentication({
                                Email: values.emailAddress,
                                Password: values.password,
                                Returnurl: returnUrl,
                            });
                        }}
                    >
                        {({
                            handleSubmit,
                            isLoading,
                        }) => (
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
                                        disabled={isLoading}
                                    >
                                        Login
                                </Button>

                                    <Nav fill>
                                        <NavItem>
                                            <NavLink href="/accounts/forgot-details">Forgot details?</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink href={registerUrl}>Sign up for LifeCMS</NavLink>
                                        </NavItem>
                                    </Nav>
                                </Form>
                            )}
                    </Formik>
                </div >
            </div>

        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(LoginFormComponent);
