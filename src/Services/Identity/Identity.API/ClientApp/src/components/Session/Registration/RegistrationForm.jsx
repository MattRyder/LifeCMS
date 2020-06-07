import React, { Component } from 'react';
import { connect } from 'react-redux';
import Fade from 'react-reveal';
import {
  Form, FormGroup, Button, Nav, NavLink, NavItem,
} from 'reactstrap';
import { Field, Formik } from 'formik';
import Schema from './RegistrationSchema';
import ReactstrapInput from '../../Util/Form';
import { createUrlWithQueryString, getParamFromSearch } from '../../../QueryString';
import { performRegistration } from '../../../redux/actions/RegistrationActions';

import '../Session.scss';
import { MessageContainer, MessageType } from '../../Util/Message/Message';

const mapStateToProps = ({ registrationState }) => ({
  isLoading: registrationState.isLoading,
  userId: registrationState.userId,
  errors: registrationState.errors,
});

const mapDispatchToProps = (dispatch) => ({
  performRegistration: (registrationParams, returnUrl) => dispatch(performRegistration(registrationParams, returnUrl)),
});

class RegistrationFormComponent extends Component {
  constructor(props) {
    super(props);

    this.state = {
      returnUrl: '',
      loginUrl: '/accounts/login'
    }
  }

  applyRedirectUri() {
    const returnUrl = getParamFromSearch(this.props, "ReturnUrl");

    const loginUrl = createUrlWithQueryString('/accounts/login', {
      ReturnUrl: returnUrl,
    });

    this.setState({
      returnUrl,
      loginUrl,
    });
  }

  componentDidMount() {
    this.applyRedirectUri();
  }

  render() {
    const { performRegistration, errors, userId } = this.props;

    const { loginUrl, returnUrl } = this.state;

    return (
      <div>
        {errors ? (
          <Fade bottom when={errors.length > 0}>
            <MessageContainer type={MessageType.error} title="Error" messages={errors} />
          </Fade>
        ) : (
            <Fade bottom when={userId.length > 0}>
              <MessageContainer type={MessageType.success} title="Your account is ready to go." messages={["Successfully created an account, you can now sign in."]} />
            </Fade>
          )
        }
        <div className="session-form">
          <div className="session-form-title">
            <span className="session-form-text">Sign Up</span>
            <p>Creating your social presence is quick and easy.</p>
          </div>
          <Formik
            initialValues={{
              emailAddress: '',
              emailAddressConfirmation: '',
              password: '',
            }}
            validationSchema={Schema}
            onSubmit={(values) => {
              performRegistration({
                Email: values.emailAddress,
                Password: values.password,
              }, returnUrl);
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
                  <FormGroup htmlFor="input-email-address-confirmation">
                    <Field
                      type="email"
                      name="emailAddressConfirmation"
                      id="input-email-address-confirmation"
                      placeholder="Confirm Email Address"
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
                    Register
            </Button>

                  <Nav fill>
                    <NavItem>
                      <NavLink href="/accounts/forgot-details">Forgot details?</NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink href={loginUrl}>Have an account?</NavLink>
                    </NavItem>
                  </Nav>
                </Form>
              )}
          </Formik>
        </div>
      </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(RegistrationFormComponent);