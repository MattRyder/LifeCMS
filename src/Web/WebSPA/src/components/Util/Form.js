/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import {
    FormFeedback, Input, Label, FormGroup,
} from 'reactstrap';

const getInputFor = (formik, inputName, label, hint) => (
    <FormGroup>
        <Label for={inputName}>{label}</Label>
        <Input
            name={inputName}
            onChange={formik.handleChange}
            invalid={formik.errors[inputName] && formik.errors[inputName].length > 0}
            value={formik.values[inputName]}
        />
        <FormFeedback valid={formik.errors[inputName] && formik.errors[inputName].length === 0} />
        { hint ? <span className="hint">{hint}</span> : null }
    </FormGroup>
);

export const getSelectFor = (formik, inputName, label, collection, hint) => (
    <FormGroup>
        <Label for={inputName}>{label}</Label>
        <Input
            type="select"
            name={inputName}
            onChange={formik.handleChange}
            invalid={formik.errors[inputName] && formik.errors[inputName].length > 0}
            value={formik.values[inputName]}
        >
            {collection && collection.map((obj, i) => (
                <option key={i} value={obj.id}>
                    {obj.name}
                </option>
            ))}
        </Input>
        { hint ? <span className="hint">{hint}</span> : null }
    </FormGroup>
);

export default getInputFor;
