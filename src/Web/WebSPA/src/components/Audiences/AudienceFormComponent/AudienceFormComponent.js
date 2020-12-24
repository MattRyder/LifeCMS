import React from 'react';
import PropTypes from 'prop-types';
import { css } from 'emotion';
import { Alert, Button } from 'reactstrap';
import { useFormik } from 'formik';
import { useTranslations } from 'hooks';
import { boxShadow } from 'theme';
import { rgba } from 'polished';
import { InitialValues } from 'components/Posts/CreatePost/CreatePostSchema';
import Icon, { Icons } from 'components/App/Iconography/Icon';
import Schema from './Schema';
import { FormikInput } from '../../Util/Form';

const styles = {
    alert: css`
        padding: 0.75rem !important;
    `,
    alertText: css`
        margin: 0 0.65rem;
    `,
    hint: css`
        color: #999;
        font-size: smaller;
    `,
    form: css`
        background-color: #fff;
        display: flex;
        flex-direction: column;
        justify-content: space-around;
        padding: 1rem;
        ${boxShadow(rgba(0, 0, 0, 0.05))}

        > div {
            padding: 0.5rem 0;
        }
    `,
};

export default function AudienceFormComponent({ audience, onFormSubmit }) {
    const { t, TextTranslationKeys } = useTranslations();

    const formik = useFormik({
        initialValues: audience,
        validateOnBlur: false,
        validateOnChange: false,
        validationSchema: Schema,
        onSubmit: ({ name }) => {
            onFormSubmit({ name });
        },
    });

    return (
        <form
            className={styles.form}
            onSubmit={formik.handleSubmit}
        >
            <FormikInput
                formik={formik}
                inputName="name"
                label={t(TextTranslationKeys.audience.properties.name)}
            />

            <Alert className={styles.alert} color="info">
                <Icon icon={Icons.info} />
                <span className={styles.alertText}>
                    You can add subscribers once the audience is created.
                </span>
            </Alert>

            <Button
                variant="default"
                type="submit"
                disabled={formik.isSubmitting}
            >
                {t(TextTranslationKeys.common.save)}
            </Button>
        </form>
    );
}

AudienceFormComponent.propTypes = {
    audience: PropTypes.shape({
        name: PropTypes.string,
    }),
};

AudienceFormComponent.defaultProps = {
    audience: InitialValues,
};
