import React from 'react';
import PropTypes from 'prop-types';
import { css, cx } from 'emotion';
import IncrementalSpinner from '../Components/Interface/IncrementalSpinner';
import AttributePanelContainer from '../Components/Interface/AttributePanelContainer';

import 'react-toggle/style.css';

const styles = {
    controls: css`
        padding: 1rem;
    `,
};

export default function SingleSpinnerAttribute({
    title, value, setValue, min, max,
}) {
    return (
        <AttributePanelContainer title={title}>
            <div className={cx(styles.controls)}>
                <IncrementalSpinner
                    min={min}
                    max={max}
                    value={value}
                    setValue={setValue}
                />
            </div>
        </AttributePanelContainer>
    );
}

SingleSpinnerAttribute.propTypes = {
    title: PropTypes.string.isRequired,
    value: PropTypes.number,
    setValue: PropTypes.func.isRequired,
    min: PropTypes.number,
    max: PropTypes.number,
};

SingleSpinnerAttribute.defaultProps = {
    min: Number.MIN_SAFE_INTEGER,
    max: Number.MAX_SAFE_INTEGER,
    value: 0,
};
