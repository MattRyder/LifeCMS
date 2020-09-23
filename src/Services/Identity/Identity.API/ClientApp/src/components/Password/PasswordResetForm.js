import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { useDispatch, useSelector } from 'react-redux';
import {
    Button,
} from 'reactstrap';
import { useFormik } from 'formik';
import { getParamFromSearch } from 'QueryString';
import { useLocation } from 'react-router';
import * as Yup from 'yup';
import { Fade } from 'react-reveal';
import MessageContainer, { MessageType } from 'components/Util/Message/Message';
import useTranslations from '../../hooks/useTranslations';
import { getInputFor } from '../Util/Form';
import { resetPassword } from '../../redux/actions/PasswordActions';
import '../Session/Session.scss';

const Schema = Yup.object().shape({
    password: Yup
        .string()
        .required('Please enter your new password'),
    passwordConfirmation: Yup
        .string()
        .oneOf(
            [Yup.ref('password')],
            'Password and the confirmation are different',
        )
        .required('Please confirm your new password'),
});

function PasswordResetForm({
    accountLoginRoute,
}) {
    const dispatch = useDispatch();

    const { search } = useLocation();

    const { t, TextTranslationKeys } = useTranslations();

    const [token, setToken] = useState('');

    const [email, setEmail] = useState('');

    const {
        passwordReset,
    } = useSelector((state) => state.passwordState);

    useEffect(() => {
        setToken(
            getParamFromSearch(search, 'token'),
        );

        setEmail(
            getParamFromSearch(search, 'email'),
        );
    }, [token, email]);

    const formik = useFormik({
        initialValues: {
            password: '',
            passwordConfirmation: '',
        },
        validationSchema: Schema,
        onSubmit: ({ password }) => dispatch(
            resetPassword(token, email, password, accountLoginRoute),
        ),
    });

    return (
        <>
            <Fade bottom when={passwordReset.reset}>
                <MessageContainer
                    type={MessageType.success}
                    title={t(TextTranslationKeys.messageContainer.success)}
                    messages={[
                        t(TextTranslationKeys.passwordResetForm.successMessage),
                    ]}
                />
            </Fade>
            <Fade bottom when={passwordReset.error}>
                <MessageContainer
                    type={MessageType.error}
                    title={t(TextTranslationKeys.messageContainer.error)}
                    messages={[passwordReset.error]}
                />
            </Fade>
            <div className="session-form">
                <div className="session-form-title">
                    <span className="session-form-text">
                        {t(TextTranslationKeys.passwordResetForm.title)}
                    </span>
                </div>
                <form onSubmit={formik.handleSubmit}>
                    {

                        getInputFor({
                            formik,
                            id: 'input-password',
                            name: 'password',
                            type: 'password',
                            autoComplete: 'new-password',
                            label: t(
                                TextTranslationKeys.passwordResetForm.password,
                            ),
                        })
                    }

                    {
                        getInputFor({
                            formik,
                            id: 'input-password-confirmation',
                            name: 'passwordConfirmation',
                            type: 'password',
                            autoComplete: 'new-password',
                            label: t(
                                TextTranslationKeys.passwordResetForm.passwordConfirmation,
                            ),
                        })
                    }

                    <Button
                        color="primary"
                        type="submit"
                        block
                        disabled={passwordReset.reset}
                    >
                        {t(TextTranslationKeys.passwordResetForm.submitButton)}
                    </Button>
                </form>
            </div>
        </>
    );
}

PasswordResetForm.propTypes = {
    accountLoginRoute: PropTypes.string.isRequired,
};

export default PasswordResetForm;
