import React from 'react';
import PropTypes from 'prop-types';
import { SketchPicker } from 'react-color';
import {
    Button,
    Input,
    InputGroup, InputGroupAddon, InputGroupText,
} from 'reactstrap';
import Icon, { Icons } from 'components/App/Iconography/Icon';
import { useToggle } from 'hooks';
import { css, cx } from 'emotion';

const ColorPreview = ({ color, onClick }) => (
    <InputGroup>
        <InputGroupAddon onClick={onClick} addonType="prepend">
            <InputGroupText>
                <Icon icon={Icons.eyedropper} />
            </InputGroupText>
        </InputGroupAddon>
        <Input
            onClick={onClick}
            style={{ backgroundColor: color }}
            bsSize="sm"
            type="text"
            readOnly
            name="color-preview"
        />
    </InputGroup>
);

ColorPreview.propTypes = {
    color: PropTypes.string.isRequired,
    onClick: PropTypes.func.isRequired,
};

const ColorInput = ({ color, setColor, onDone }) => {
    const styles = {
        colorPicker: css`
        display: flex;
        flex-direction: row;
    `,
    };

    return (
        <div className={cx(styles.colorPicker)}>
            <SketchPicker color={color} onChange={setColor} />
            <Button
                type="button"
                color="primary"
                size="sm"
                onClick={onDone}
            >
                <Icon icon={Icons.check} />
            </Button>
        </div>
    );
};

ColorInput.propTypes = {
    color: PropTypes.string.isRequired,
    setColor: PropTypes.func.isRequired,
    onDone: PropTypes.func.isRequired,
};

export default function ColorPicker({
    color,
    setColor,
}) {
    const [isPickerOpen, togglePickerOpen] = useToggle(false);

    const handleInputClick = () => {
        togglePickerOpen();
    };

    return (
        isPickerOpen ? (
            <ColorInput color={color} setColor={setColor} onDone={handleInputClick} />
        ) : <ColorPreview onClick={handleInputClick} color={color} />
    );
}

ColorPicker.propTypes = {
    color: PropTypes.string.isRequired,
    setColor: PropTypes.func.isRequired,
};
