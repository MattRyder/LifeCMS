import React from 'react';
import IncrementalSpinner from '../Components/Interface/IncrementalSpinner';
import AttributePanelContainer from '../Components/Interface/AttributePanelContainer';

import 'react-toggle/style.css';

export default function SingleSpinnerAttribute({
    title, value, setValue, min, max,
}) {
    return (
        <div className="attribute">
            <AttributePanelContainer title={title}>
                <div className="controls">
                    <IncrementalSpinner
                        min={min}
                        max={max}
                        value={value}
                        setValue={setValue}
                    />
                </div>
            </AttributePanelContainer>
        </div>
    );
}
