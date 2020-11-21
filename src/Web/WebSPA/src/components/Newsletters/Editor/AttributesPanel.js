import React from 'react';
import { cx, css } from 'emotion';
import { rgba } from 'polished';
import { useEditor } from '@craftjs/core';
import { useTranslations } from 'hooks';
import { Icons } from '../../App/Iconography/Icon';
import AttributePanelButton from './Components/Interface/AttributePanelButton';

import './AttributesPanel.scss';

const styles = {
    content: css`
        border-bottom: 3px dotted ${rgba(0, 0, 0, 0.25)};
    `,
    headerRow: css`
        background-color: darken(#f5f5f5, 10%);
        border-bottom: 1px solid rgba(0, 0, 0, 0.2);
        display: flex;
        list-style-type: none;
        margin-bottom: 0;
        padding: 0;
        justify-content: space-evenly;

        li {
            padding: 0.25rem 0.5rem;
        }
    `,
};

export default function AttributesPanel() {
    const { t, TextTranslationKeys } = useTranslations();

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
                <ul className={cx(styles.headerRow)}>
                    <li>
                        <AttributePanelButton
                            id="delete"
                            icon={Icons.trash}
                            text={t(TextTranslationKeys.common.delete)}
                            className="text-danger"
                            onClick={() => actions.delete(selected.id)}
                        />
                    </li>
                    <li>
                        <AttributePanelButton
                            id="duplicate"
                            icon={Icons.clone}
                            text={t(TextTranslationKeys.common.duplicate)}
                        />
                    </li>
                </ul>
            ) : null}
            <div className={cx(styles.content)}>
                { selected.attributesPanel && React.createElement(selected.attributesPanel) }
            </div>
        </div>
    );
}
