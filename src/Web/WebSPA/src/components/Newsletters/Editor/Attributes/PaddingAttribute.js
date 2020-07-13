import React, { useState, useEffect } from 'react';
import Toggle from 'react-toggle';
import IncrementalSpinner from '../Components/Interface/IncrementalSpinner';
import AttributePanelContainer from '../Components/Interface/AttributePanelContainer';
import useBasicMode from '../Hooks/useBasicMode';

import 'react-toggle/style.css';
import './PaddingAttribute.scss';

export default function PaddingAttribute({ handleChange }) {
    const [paddingTop, setPaddingTop] = useState(1);
    const [paddingLeft, setPaddingLeft] = useState(1);
    const [paddingRight, setPaddingRight] = useState(1);
    const [paddingBottom, setPaddingBottom] = useState(1);

    const [isBasicMode, toggleBasicMode] = useBasicMode();

    useEffect(() => {
        handleChange([paddingTop, paddingLeft, paddingRight, paddingBottom]);
    }, [handleChange, paddingTop, paddingLeft, paddingRight, paddingBottom]);

    const setAll = (value) => {
        setPaddingTop(value);
        setPaddingLeft(value);
        setPaddingRight(value);
        setPaddingBottom(value);
    };

    return (
        <div className="padding-attribute">
            <AttributePanelContainer title="Padding">
                <span className="mode">
                    {isBasicMode ? 'Basic' : 'Advanced'}
                    <Toggle
                        icons={false}
                        defaultChecked={isBasicMode}
                        onChange={toggleBasicMode}
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
