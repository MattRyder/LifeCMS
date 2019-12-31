import React from 'react';
import {
    Form, FormGroup, Button, Nav, NavLink, NavItem,
} from 'reactstrap';
import { Formik, Field } from 'formik';
import * as Yup from 'yup';
import ReactstrapInput from '../Util/Form';

import './Session.scss';

const LoginFormSchema = Yup.object().shape({
    emailAddress: Yup
        .string()
        .email('Please enter a valid email address')
        .required('Please enter an email address'),
});

export default function ForgotPasswordForm() {
    const handleSubmit = (actions, values) => {
        console.log(`What values: ${this.values}`);
    };

    return (
        <div className="session-form">
            <div className="session-form-title">
                <span className="session-form-text">Forgot your details?</span>
                <p>Please enter your email address to request a password reset.</p>
            </div>
            <Formik
                initialValues={{
                    emailAddress: '',
                    password: '',
                }}
                validationSchema={LoginFormSchema}
                onSubmit={(values) => {
                    console.log(values);
                }}
            >
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
                  
                    <Button variant="primary" className="btn-submit" type="submit" block>
                        Start Recovery
                    </Button>

                    <Nav fill>
                        <NavItem>
                            <NavLink href="/accounts/login">Have an account?</NavLink>
                        </NavItem>
                    </Nav>
                </Form>
            </Formik>
        </div>
    );
}
