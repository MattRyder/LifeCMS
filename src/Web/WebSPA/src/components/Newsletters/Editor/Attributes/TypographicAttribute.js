import { css, cx } from 'emotion';
import React from 'react';
import { ButtonGroup, Input } from 'reactstrap';

import { Icons } from '../../../App/Iconography/Icon';
import AttributePanelButton from '../Components/Interface/AttributePanelButton';
import AttributePanelContainer from '../Components/Interface/AttributePanelContainer';

const styles = {
    controls: css`
        padding: 1rem;
    `,
};

export default function TypographicAttribute({
    bold, italic, underline, text, handleChange,
}) {
    return (
        <AttributePanelContainer title="Text">
            <ButtonGroup>
                <AttributePanelButton
                    id="bold"
                    icon={Icons.bold}
                    text="Bold"
                    active={bold}
                    onClick={() => handleChange('bold', !bold)}
                />
                <AttributePanelButton
                    id="italic"
                    icon={Icons.italic}
                    text="Italic"
                    active={italic}
                    onClick={() => handleChange('italic', !italic)}
                />
                <AttributePanelButton
                    id="underline"
                    icon={Icons.underline}
                    text="Underline"
                    active={underline}
                    onClick={() => handleChange('underline', !underline)}
                />
            </ButtonGroup>

            <div className={cx(styles.controls)}>
                <Input
                    placeholder="Text"
                    type="textarea"
                    value={text}
                    onChange={(e) => handleChange('text', e.currentTarget.value)}
                />
            </div>

        </AttributePanelContainer>
    );
}
