import React from 'react';
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
