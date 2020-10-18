import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import IncrementalSpinner from '../Components/Interface/IncrementalSpinner';
import AttributePanelContainer from '../Components/Interface/AttributePanelContainer';
import useBasicMode from '../Hooks/useBasicMode';

import 'react-toggle/style.css';
import BasicModeToggle from './BasicModeToggle';

const styles = {
    controls: css`
        padding: 1rem;
    `,
    advanced: css`
        display: grid;
        grid-template-columns: repeat(4, 1fr);
        grid-template-rows: repeat(3, 1fr);
        grid-column-gap: 1em;
        grid-row-gap: 1em;
        > :nth-child(1) { grid-area: 1 / 2 / 2 / 4; }
        > :nth-child(4) { grid-area: 2 / 1 / 3 / 3; }
        > :nth-child(2) { grid-area: 2 / 3 / 3 / 5; }
        > :nth-child(3) { grid-area: 3 / 2 / 4 / 4; } 
    `,
};

function PaddingAttribute({
    values, handleChange,
}) {
    const [paddingTop, setPaddingTop] = useState(values[0]);
    const [paddingLeft, setPaddingLeft] = useState(values[1]);
    const [paddingRight, setPaddingRight] = useState(values[2]);
    const [paddingBottom, setPaddingBottom] = useState(values[3]);

    const allValuesEqual = (paddingTop === paddingLeft) === (paddingRight === paddingBottom);

    const [isBasicMode, toggleBasicMode] = useBasicMode(allValuesEqual);

    useEffect(() => {
        handleChange([paddingTop, paddingLeft, paddingRight, paddingBottom]);
    }, [handleChange, paddingTop, paddingLeft, paddingRight, paddingBottom]);

    const setAll = (value) => {
        setPaddingTop(value);
        setPaddingLeft(value);
        setPaddingRight(value);
        setPaddingBottom(value);
    };

    const handleBasicModeToggle = () => {
        if (isBasicMode) {
            setAll(paddingTop);
        }

        toggleBasicMode();
    };

    return (
        <AttributePanelContainer title="Padding">
            <BasicModeToggle
                isBasicMode={isBasicMode}
                handleToggle={handleBasicModeToggle}
            />
            <div className={cx(styles.controls)}>
                { isBasicMode ? (
                    <IncrementalSpinner
                        value={paddingTop}
                        setValue={setAll}
                    />
                ) : (
                    <div className={styles.advanced}>
                        <IncrementalSpinner
                            value={paddingTop}
                            setValue={(value) => setPaddingTop(value)}
                        />
                        <IncrementalSpinner
                            value={paddingLeft}
                            setValue={(value) => setPaddingLeft(value)}
                        />
                        <IncrementalSpinner
                            value={paddingRight}
                            setValue={(value) => setPaddingRight(value)}
                        />
                        <IncrementalSpinner
                            value={paddingBottom}
                            setValue={(value) => setPaddingBottom(value)}
                        />
                    </div>
                ) }
            </div>
        </AttributePanelContainer>
    );
}

PaddingAttribute.propTypes = {
    values: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.arrayOf(PropTypes.number),
    ]).isRequired,
    handleChange: PropTypes.func.isRequired,
};

export default PaddingAttribute;
