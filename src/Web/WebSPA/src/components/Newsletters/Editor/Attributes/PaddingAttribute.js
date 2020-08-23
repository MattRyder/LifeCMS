import React, { useState, useEffect } from 'react';
import Toggle from 'react-toggle';
import IncrementalSpinner from '../Components/Interface/IncrementalSpinner';
import AttributePanelContainer from '../Components/Interface/AttributePanelContainer';
import useBasicMode from '../Hooks/useBasicMode';

import 'react-toggle/style.css';
import './PaddingAttribute.scss';

export default function PaddingAttribute({
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

    const toggleAndSet = () => {
        if (isBasicMode) {
            setAll(paddingTop);
        }

        toggleBasicMode();
    };

    return (
        <div className="padding-attribute">
            <AttributePanelContainer title="Padding">
                <span className="mode">
                    {isBasicMode ? 'Basic' : 'Advanced'}
                    <Toggle
                        icons={false}
                        defaultChecked={isBasicMode}
                        onChange={toggleAndSet}
                    />
                </span>
                <div className="controls">
                    { isBasicMode ? (
                        <IncrementalSpinner
                            value={paddingTop}
                            setValue={setAll}
                        />
                    ) : (
                        <div>
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
        </div>
    );
}
