import React from 'react';
import {
    InputGroup, InputGroupAddon, Button, Input,
} from 'reactstrap';

export default function IncrementalSpinner({ value, setValue }) {
    const increment = () => setValue(value + 1);

    const decrement = () => setValue(value - 1);

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
                    onChange={(e) => setValue(e.currentTarget.value)}
                />

                <InputGroupAddon addonType="append">
                    <Button onClick={increment}>+</Button>
                </InputGroupAddon>
            </InputGroup>
        </div>
    );
}
