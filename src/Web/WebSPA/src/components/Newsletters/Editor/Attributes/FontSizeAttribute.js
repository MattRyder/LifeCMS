import React, { useState, useEffect } from 'react';
import IncrementalSpinner from '../Components/Interface/IncrementalSpinner';
import AttributePanelContainer from '../Components/Interface/AttributePanelContainer';

import 'react-toggle/style.css';

export default function FontsizeAttribute({ handleChange }) {
    const [fontSize, setFontSize] = useState(1);

    useEffect(() => {
        handleChange(fontSize);
    }, [handleChange, fontSize]);

    return (
        <div className="font-size-attribute">
            <AttributePanelContainer title="Font Size">
                <div className="controls">
                    <IncrementalSpinner
                        value={fontSize}
                        setValue={setFontSize}
                    />
                </div>
            </AttributePanelContainer>
        </div>
    );
}
