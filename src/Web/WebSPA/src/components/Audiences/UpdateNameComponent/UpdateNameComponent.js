import React from 'react';
import { Button } from 'reactstrap';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import {
    useTranslations,
} from 'hooks';
import { FormikInput } from 'components/Util/Form';
import theme from 'theme';

const Schema = Yup.object().shape({
    name: Yup.string().required(),
});

export default function UpdateNameComponent({
    name, handleSubmit,
}) {
    const { t, TextTranslationKeys } = useTranslations();

    const formik = useFormik({
        initialValues: { name },
        validationSchema: Schema,
        validateOnBlur: false,
        validateOnChange: false,
        onSubmit: handleSubmit,
    });

    return (
        <form
            className={theme.components.pageStyleForm}
            onSubmit={formik.handleSubmit}
        >
            <FormikInput
                formik={formik}
                inputName="name"
                label={t(TextTranslationKeys.audience.properties.name)}
            />

            <Button variant="default" type="submit" disabled={formik.isSubmitting}>
                {t(TextTranslationKeys.common.save)}
            </Button>
        </form>
    );
}
