import React from 'react';
import PropTypes from 'prop-types';
import { useDispatch, useSelector } from 'react-redux';
import Fade from 'react-reveal';
import {
    Button,
} from 'reactstrap';
import { useFormik } from 'formik';
import InternalNavigation from 'components/InternalNavigation';
import { getParamFromSearch } from 'QueryString';
import { useLocation } from 'react-router';
import useTranslations from '../../../hooks/useTranslations';
import { getInputFor } from '../../Util/Form';
import Schema, { InitialValues } from './LoginSchema';
import { performAuthentication } from '../../../redux/actions/AuthenticationActions';
import MessageContainer, { MessageType } from '../../Util/Message/Message';

import '../Session.scss';

export function LoginFormComponent({
    accountRegistrationRoute,
    forgotPasswordRoute,
}) {
    const {
        authentication: { isLoading, errors },
        registration: { userId },
    } = useSelector(
        (state) => ({
            authentication: state.authenticationState,
            registration: state.registrationState,
        }),
    );

    const dispatch = useDispatch();

    const { search } = useLocation();

    const { t, TextTranslationKeys } = useTranslations();

    const formik = useFormik({
        initialValues: InitialValues,
        validationSchema: Schema,
        onSubmit: ({ emailAddress, password }) => {
            dispatch(performAuthentication({
                Email: emailAddress,
                Password: password,
                ReturnUrl: getParamFromSearch(search, 'ReturnUrl'),
            }));
        },
    });

    return (
        <>
            {errors && (
                <Fade bottom when={errors.length > 0}>
                    <MessageContainer
                        type={MessageType.error}
                        title={t(TextTranslationKeys.messageContainer.error)}
                        messages={errors}
                    />
                </Fade>
            )}


            {userId && (
                <Fade bottom when={userId.length > 0}>
                    <MessageContainer
                        type={MessageType.success}
                        title={t(TextTranslationKeys.messageContainer.success)}
                        messages={[
                            t(TextTranslationKeys.loginForm.successMessage),
                        ]}
                    />
                </Fade>
            )}


            <div className="session-form">
                <div className="session-form-title">
                    <span className="session-form-text">
                        {t(TextTranslationKeys.loginForm.title)}
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
                                TextTranslationKeys.loginForm.emailAddressPlaceholder,
                            ),
                        })
                    }

                    {
                        getInputFor({
                            formik,
                            id: 'input-password',
                            name: 'password',
                            type: 'password',
                            label: t(
                                TextTranslationKeys.loginForm.passwordPlaceholder,
                            ),
                        })
                    }

                    <Button
                        color="primary"
                        type="submit"
                        block
                        disabled={isLoading}
                    >
                        {t(TextTranslationKeys.loginForm.submitButton)}
                    </Button>

                    <InternalNavigation links={[
                        {
                            href: forgotPasswordRoute,
                            text: t(TextTranslationKeys.cta.forgotDetails),
                        },
                        {
                            href: accountRegistrationRoute,
                            text: t(TextTranslationKeys.cta.register),
                        },
                    ]}
                    />
                </form>
            </div>
        </>
    );
}

LoginFormComponent.propTypes = {
    accountRegistrationRoute: PropTypes.string.isRequired,
    forgotPasswordRoute: PropTypes.string.isRequired,
};

export default LoginFormComponent;
