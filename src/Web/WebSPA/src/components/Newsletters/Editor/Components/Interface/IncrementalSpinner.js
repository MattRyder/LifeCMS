import React from 'react';
import PropTypes from 'prop-types';
import {
    InputGroup, InputGroupAddon, Button, Input,
} from 'reactstrap';

function IncrementalSpinner({
    value,
    setValue,
    min,
    max,
}) {
    const increment = () => setValue(value < max ? value + 1 : max);

    const decrement = () => setValue(value > min ? value - 1 : min);

    return (
        <InputGroup>
            <InputGroupAddon addonType="prepend">
                <Button size="sm" onClick={decrement}>-</Button>
            </InputGroupAddon>

            <Input
                bsSize="sm"
                type="text"
                pattern="\d*"
                name="padding"
                id="input-padding"
                value={value}
                min="0"
                max="100"
                onChange={(e) => setValue(e.currentTarget.value)}
            />

            <InputGroupAddon addonType="append">
                <Button size="sm" onClick={increment}>+</Button>
            </InputGroupAddon>
        </InputGroup>
    );
}

IncrementalSpinner.propTypes = {
    min: PropTypes.number,
    max: PropTypes.number,
    value: PropTypes.number,
    setValue: PropTypes.func.isRequired,
};

IncrementalSpinner.defaultProps = {
    min: Number.MIN_SAFE_INTEGER,
    max: Number.MAX_SAFE_INTEGER,
    value: 0,
};

export default IncrementalSpinner;
