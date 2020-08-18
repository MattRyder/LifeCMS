import React from 'react';
import { ButtonGroup } from 'reactstrap';

import { Icons } from '../../../App/Iconography/Icon';
import AttributePanelButton from '../Components/Interface/AttributePanelButton';
import AttributePanelContainer from '../Components/Interface/AttributePanelContainer';

export default function TypographicAttribute({
    bold, italic, underline, handleChange,
}) {
    return (
        <div className="typographic-attribute">
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
            </AttributePanelContainer>
        </div>
    );
}
