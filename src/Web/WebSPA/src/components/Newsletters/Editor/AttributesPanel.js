import React from 'react';
import { useEditor } from '@craftjs/core';
import { Icons } from '../../App/Iconography/Icon';
import AttributePanelButton from './Components/Interface/AttributePanelButton';

import './AttributesPanel.scss';

export default function AttributesPanel() {
    const { actions, selected } = useEditor((state, query) => {
        const currentNodeId = state.events.selected;

        let selectedNode = {};

        if (currentNodeId) {
            selectedNode = {
                id: currentNodeId,
                name: state.nodes[currentNodeId].data.name,
                attributesPanel:
                    state.nodes[currentNodeId].related
                    && state.nodes[currentNodeId].related.attributesPanel,
                isDeletable: query.node(currentNodeId).isDeletable(),
            };
        }

        return {
            selected: selectedNode,
        };
    });

    return (
        <div className="attributes-panel">
            { selected ? (
                <ul className="header-row">
                    <li>
                        <AttributePanelButton
                            id="delete"
                            icon={Icons.trash}
                            text="Delete"
                            className="text-danger"
                            onClick={() => actions.delete(selected.id)}
                        />
                    </li>
                    <li>
                        <AttributePanelButton
                            id="duplicate"
                            icon={Icons.clone}
                            text="Duplicate"
                        />
                    </li>
                </ul>
            ) : null}
            { selected.attributesPanel && React.createElement(selected.attributesPanel) }
        </div>
    );
}
