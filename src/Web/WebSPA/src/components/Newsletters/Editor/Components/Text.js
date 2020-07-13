import React from 'react';
import { useNode } from '@craftjs/core';
import { FontSizeAttribute, PaddingAttribute } from '../Attributes';

export default function Text({ fontSize, padding, text }) {
    const {
        connectors: { connect, drag },
        isSelected,
    } = useNode((state) => ({
        dragged: state.events.dragged,
        isSelected: state.events.selected,
    }));

    return (
        <div
            ref={(ref) => connect(drag(ref))}
            className={`page-component ${isSelected ? 'is-selected' : ''}`}
        >
            <p style={{
                fontSize: `${fontSize}rem`,
                padding: `${padding[0]}rem ${padding[1]}rem ${padding[2]}rem ${padding[3]}rem`,
            }}
            >
                {text}
            </p>
        </div>
    );
}

export function TextAttributesPanel() {
    const {
        actions: { setProp },
    } = useNode();

    return (
        <div className="text-attributes-panel">
            <FontSizeAttribute
                handleChange={(value) => setProp(
                    (props) => (props.fontSize = value),
                )}
            />

            <PaddingAttribute
                handleChange={(value) => setProp(
                    (props) => (props.padding = value),
                )}
            />
        </div>
    );
}

Text.craft = {
    props: {
        fontSize: 1,
        padding: [1, 1, 1, 1],
        text: 'Hello, World!',
    },
    related: {
        attributesPanel: TextAttributesPanel,
    },
};
