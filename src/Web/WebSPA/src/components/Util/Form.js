/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import PropTypes from 'prop-types';
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

export const FormikInput = ({
    formik, inputName, label, hint,
}) => (
    <FormGroup>
        <Label for={inputName}>{label}</Label>
        <Input
            name={inputName}
            onChange={formik.handleChange}
            invalid={formik.errors[inputName] && formik.errors[inputName].length > 0}
            value={formik.values && formik.values[inputName]}
        />
        <FormFeedback valid={formik.errors[inputName] && formik.errors[inputName].length === 0} />
        { hint ? <span className="hint">{hint}</span> : null }
    </FormGroup>
);

FormikInput.propTypes = {
    // eslint-disable-next-line react/forbid-prop-types
    formik: PropTypes.object,
    inputName: PropTypes.string,
    label: PropTypes.string,
    hint: PropTypes.string,
};

FormikInput.defaultProps = {
    formik: {},
    inputName: '',
    label: '',
    hint: '',
};

export const FormikSelect = ({
    formik,
    inputName,
    label,
    collection,
    hint,
    inputClassName,
}) => {
    const isInvalid = formik.errors[inputName]
        && formik.errors[inputName].length > 0;

    return (
        <FormGroup>
            <Label for={inputName}>{label}</Label>
            <Input
                type="select"
                name={inputName}
                className={inputClassName}
                onChange={formik.handleChange}
                invalid={isInvalid}
                value={formik.values[inputName]}
            >
                <option selected value />
                {collection && collection.map((obj, i) => (
                    <option key={i} value={obj.id}>
                        {obj.name}
                    </option>
                ))}
            </Input>
            { hint ? <span className="hint">{hint}</span> : null }
        </FormGroup>
    );
};

export default getInputFor;
