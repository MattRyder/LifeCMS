import React from 'react';
import { UncontrolledTooltip, Button } from 'reactstrap';
import Icon from '../../../../App/Iconography/Icon';

import '../../../../../assets/styles/accessibility.scss';

export default function AttributePanelButton({
    id, icon, onClick, text, active,
}) {
    const buttonId = `tooltip-${id}`;

    return (
        <div>
            <UncontrolledTooltip target={buttonId}>
                {text}
            </UncontrolledTooltip>
            <Button
                color="link"
                id={buttonId}
                type="button"
                active={active}
                onClick={onClick}
            >
                <p className="a11y-visually-hidden">{text}</p>
                <Icon icon={icon} />
            </Button>
        </div>
    );
}
