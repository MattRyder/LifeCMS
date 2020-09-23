import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import { useDispatch, useSelector } from 'react-redux';
import Fade from 'react-reveal';
import {
    Button,
} from 'reactstrap';
import { useFormik } from 'formik';
import { useLocation } from 'react-router';
import { performRegistration } from 'redux/actions/RegistrationActions';
import InternalNavigation from 'components/InternalNavigation';
import { push } from 'connected-react-router';
import useTranslations from '../../../hooks/useTranslations';
import { getInputFor } from '../../Util/Form';
import { makeReturnHref } from '../../../QueryString';
import Schema, { InitialValues } from './RegistrationSchema';
import MessageContainer, { MessageType } from '../../Util/Message/Message';

import '../Session.scss';

function RegistrationFormComponent({
    accountLoginRoute,
    forgotPasswordRoute,
}) {
    const { search } = useLocation();

    const { isLoading, errors, userId } = useSelector(
        (state) => state.registrationState,
    );

    const dispatch = useDispatch();

    const { t, TextTranslationKeys } = useTranslations();

    useEffect(() => {
        if (userId) {
            setTimeout(
                () => dispatch(push(makeReturnHref(search, accountLoginRoute))),
                1000 * 1.5,
            );
        }
    }, [dispatch, userId]);

    const formik = useFormik({
        initialValues: InitialValues,
        validationSchema: Schema,
        onSubmit: ({ emailAddress, password }) => {
            dispatch(performRegistration({
                Email: emailAddress,
                Password: password,
            }, makeReturnHref(search, accountLoginRoute)));
        },
    });

    return (
        <>
            <Fade bottom when={errors.length > 0}>
                <MessageContainer
                    type={MessageType.error}
                    title={t(TextTranslationKeys.messageContainer.error)}
                    messages={errors}
                />
            </Fade>
            <div className="session-form">
                <div className="session-form-title">
                    <span className="session-form-text">
                        {t(TextTranslationKeys.registrationForm.title)}
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
                                TextTranslationKeys.registrationForm.emailAddress,
                            ),
                        })
                    }

                    {
                        getInputFor({
                            formik,
                            id: 'input-email-address-confirm',
                            name: 'emailAddressConfirmation',
                            type: 'email',
                            label: t(
                                TextTranslationKeys.registrationForm.emailAddressConfirm,
                            ),
                        })
                    }

                    <hr />

                    {
                        getInputFor({
                            formik,
                            id: 'input-password',
                            name: 'password',
                            type: 'password',
                            label: t(
                                TextTranslationKeys.registrationForm.password,
                            ),
                        })
                    }

                    <Button
                        color={`${userId ? 'success' : 'primary'}`}
                        type="submit"
                        block
                        disabled={isLoading}
                    >
                        {t(TextTranslationKeys.registrationForm.submitButton)}
                    </Button>
                    <InternalNavigation links={[
                        {
                            href: forgotPasswordRoute,
                            text: t(TextTranslationKeys.cta.forgotDetails),
                        },
                        {
                            href: accountLoginRoute,
                            text: t(TextTranslationKeys.cta.login),
                        },
                    ]}
                    />
                </form>
            </div>
        </>
    );
}

RegistrationFormComponent.propTypes = {
    accountLoginRoute: PropTypes.string.isRequired,
    forgotPasswordRoute: PropTypes.string.isRequired,
};

export default RegistrationFormComponent;
