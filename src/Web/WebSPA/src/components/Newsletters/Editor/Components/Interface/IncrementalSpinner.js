import React from 'react';
import {
    InputGroup, InputGroupAddon, Button, Input,
} from 'reactstrap';

export default function IncrementalSpinner({
    value,
    setValue,
    min = Number.MIN_SAFE_INTEGER,
    max = Number.MAX_SAFE_INTEGER,
}) {
    const increment = () => setValue(value < max ? value + 1 : max);

    const decrement = () => setValue(value > min ? value - 1 : min);

    return (
        <div className="incremental-spinner">
            <InputGroup>
                <InputGroupAddon addonType="prepend">
                    <Button onClick={decrement}>-</Button>
                </InputGroupAddon>

                <Input
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
                    <Button onClick={increment}>+</Button>
                </InputGroupAddon>
            </InputGroup>
        </div>
    );
}
