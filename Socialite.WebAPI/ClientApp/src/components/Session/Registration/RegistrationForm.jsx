import React from 'react';
import { connect } from 'react-redux';
import {
  Form, FormGroup, Button, Nav, NavLink, NavItem,
} from 'reactstrap';
import { Field, Formik } from 'formik';
import Schema from './RegistrationSchema';
import ReactstrapInput from '../../Util/Form';
import { performRegistration } from '../../../redux/actions/RegistrationActions';

import '../Session.scss';

const mapDispatchToProps = (dispatch) => ({
  performRegistration: (registrationParams) => dispatch(performRegistration(registrationParams)),
});

const RegistrationFormComponent = ({ performRegistration }) => (
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
      onSubmit={(values, { setSubmitting }) => {
        performRegistration({
          Email: values.emailAddress,
          Password: values.password,
        });
      }}
    >
      {({
        handleSubmit,
        isSubmitting,
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
              disabled={isSubmitting}
            >
              Register
            </Button>

            <Nav fill>
              <NavItem>
                <NavLink href="/accounts/forgot-details">Forgot details?</NavLink>
              </NavItem>
              <NavItem>
                <NavLink href="/accounts/login">Have an account?</NavLink>
              </NavItem>
            </Nav>
          </Form>
        )}
    </Formik>
  </div >
);

export default connect(null, mapDispatchToProps)(RegistrationFormComponent);

