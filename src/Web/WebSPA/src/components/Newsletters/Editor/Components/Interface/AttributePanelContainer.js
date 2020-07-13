import React, { useState } from 'react';
import { Collapse } from 'reactstrap';
import Icon, { Icons } from '../../../../App/Iconography/Icon';

import './AttributePanelContainer.scss';

export default function AttributePanelContainer({ title, children }) {
    const [isOpen, setIsOpen] = useState(true);

    const toggleIsOpen = () => setIsOpen(!isOpen);

    return (
        <div className="attribute-panel-container">
            <div className="header">
                <button type="button" className="header-trigger" onClick={toggleIsOpen}>
                    <span>
                        {title}
                        <Icon icon={isOpen ? Icons.caretUp : Icons.caretDown} />
                    </span>
                </button>
            </div>

            <Collapse isOpen={isOpen}>
                {children}
            </Collapse>
        </div>
    );
}
