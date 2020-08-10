/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import {
    FormFeedback, Input,
} from 'reactstrap';

const ReactstrapInput = ({ field, form: { touched, errors }, ...props }) => (
    <div>
        <Input
            invalid={!!(touched[field.name] && errors[field.name])}
            {...field}
            {...props}
        />
        {touched[field.name] && errors[field.name] && <FormFeedback>{errors[field.name]}</FormFeedback>}
    </div>
);

export default ReactstrapInput;
