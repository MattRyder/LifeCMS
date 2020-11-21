import React from 'react';
import PropTypes from 'prop-types';
import { css, cx } from 'emotion';
import AttributePanelContainer from '../Components/Interface/AttributePanelContainer';
import ColorPicker from '../Components/Interface/ColorPicker';

const styles = {
    controls: css`
        padding: 1rem;
    `,
};

export default function ColorAttribute({
    title, color, handleChange,
}) {
    return (
        <AttributePanelContainer title={title}>
            <div className={cx(styles.controls)}>
                <ColorPicker
                    color={color}
                    setColor={(col) => handleChange(col.hex)}
                />
            </div>
        </AttributePanelContainer>
    );
}

ColorAttribute.propTypes = {
    title: PropTypes.string.isRequired,
    color: PropTypes.string.isRequired,
    handleChange: PropTypes.func.isRequired,
};
