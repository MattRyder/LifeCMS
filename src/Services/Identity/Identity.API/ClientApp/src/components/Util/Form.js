import React from 'react';
import {
    FormFeedback, Input, FormGroup, Label, FormText,
} from 'reactstrap';

// eslint-disable-next-line import/prefer-default-export
export const getInputFor = ({
    formik: {
        handleChange, handleBlur, errors, values, touched,
    },
    id, label, name, type, hint, autoComplete,
}) => (
    <FormGroup>
        {label && <Label htmlFor={id}>{label}</Label>}
        <Input
            id={id}
            name={name}
            autoComplete={autoComplete}
            onBlur={handleBlur}
            onChange={handleChange}
            invalid={!!(touched[name] && errors[name])}
            type={type}
            value={values[name]}
        />
        {touched[name] && errors[name] && <FormFeedback>{errors[name]}</FormFeedback>}
        {hint && <FormText>{hint}</FormText>}
    </FormGroup>
);
