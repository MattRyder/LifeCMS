import React from 'react';
import PropTypes from 'prop-types';
import {
    Button, Nav, NavLink, NavItem,
} from 'reactstrap';
import Fade from 'react-reveal';
import { useFormik } from 'formik';
import * as Yup from 'yup';

import { makeReturnHref } from 'QueryString';
import { useDispatch, useSelector } from 'react-redux';
import { useLocation } from 'react-router';
import useTranslations from 'hooks/useTranslations';
import { getInputFor } from 'components/Util/Form';
import { requestPasswordReset } from 'redux/actions/PasswordActions';

import './Session.scss';
import MessageContainer, { MessageType } from 'components/Util/Message/Message';

const ForgotPasswordSchema = Yup.object().shape({
    emailAddress: Yup
        .string()
        .email('Please enter a valid email address')
        .required('Please enter an email address'),
});

function ForgotPasswordForm({
    accountRegistrationRoute,
    accountLoginRoute,
}) {
    const { t, TextTranslationKeys } = useTranslations();

    const { search } = useLocation();

    const dispatch = useDispatch();

    const {
        passwordRequest,
    } = useSelector((state) => state.passwordState);

    const formik = useFormik({
        initialValues: {
            emailAddress: '',
        },
        validationSchema: ForgotPasswordSchema,
        onSubmit: (values) => {
            dispatch(requestPasswordReset(
                values.emailAddress,
                makeReturnHref(accountLoginRoute),
            ));
        },
    });

    return (
        <>
            <Fade bottom when={passwordRequest.requested}>
                <MessageContainer
                    type={MessageType.success}
                    title={t(TextTranslationKeys.messageContainer.success)}
                    messages={[
                        t(TextTranslationKeys.forgotPasswordForm.successMessage),
                    ]}
                />
            </Fade>

            <div className="session-form">
                <div className="session-form-title">
                    <span className="session-form-text">
                        {t(TextTranslationKeys.forgotPasswordForm.title)}
                    </span>
                </div>
                <form onSubmit={formik.handleSubmit}>
                    {
                        getInputFor({
                            formik,
                            id: 'input-email-address',
                            name: 'emailAddress',
                            type: 'email',
                            label: t(
                                TextTranslationKeys.forgotPasswordForm.emailAddress,
                            ),
                        })
                    }
                    <Button
                        color="primary"
                        className="btn-submit"
                        type="submit"
                        block
                        disabled={passwordRequest.requested}
                    >
                        {t(TextTranslationKeys.forgotPasswordForm.submitButton)}
                    </Button>

                    <Nav fill>
                        <NavItem>
                            <NavLink href={makeReturnHref(search, accountRegistrationRoute)}>
                                {t(TextTranslationKeys.cta.register)}
                            </NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink href={makeReturnHref(search, accountLoginRoute)}>
                                {t(TextTranslationKeys.cta.login)}
                            </NavLink>
                        </NavItem>
                    </Nav>
                </form>
            </div>
        </>
    );
}

ForgotPasswordForm.propTypes = {
    accountRegistrationRoute: PropTypes.string.isRequired,
    accountLoginRoute: PropTypes.string.isRequired,
};

export default ForgotPasswordForm;
