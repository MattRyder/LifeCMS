import React from 'react';
import { useNode } from '@craftjs/core';
import { FontSizeAttribute, PaddingAttribute } from '../Attributes';
import TypographicAttribute from '../Attributes/TypographicAttribute';

export default function Text({
    bold, italic, underline, fontSize, padding, text,
}) {
    const {
        connectors: { connect, drag },
        isSelected,
    } = useNode((state) => ({
        dragged: state.events.dragged,
        isSelected: state.events.selected,
    }));

    const styles = {
        fontSize: `${fontSize}rem`,
        padding: `${padding[0]}rem ${padding[1]}rem ${padding[2]}rem ${padding[3]}rem`,
    };

    if (bold) { styles.fontWeight = 'bold'; }

    if (italic) { styles.fontStyle = 'italic'; }

    if (underline) { styles.textDecoration = 'underline'; }

    return (
        <div
            ref={(ref) => connect(drag(ref))}
            className={`page-component ${isSelected ? 'is-selected' : ''}`}
        >
            <p style={styles}>
                {text}
            </p>
        </div>
    );
}

export function TextAttributesPanel() {
    const {
        actions: { setProp },
        props,
    } = useNode((node) => ({
        props: node.data.props,
    }));

    return (
        <div className="text-attributes-panel">
            <FontSizeAttribute
                value={props.fontSize}
                handleChange={(value) => setProp(
                    (props) => (props.fontSize = value),
                )}
            />

            <PaddingAttribute
                values={props.padding}
                handleChange={(value) => setProp(
                    (props) => (props.padding = value),
                )}
            />

            <TypographicAttribute
                bold={props.bold}
                italic={props.italic}
                underline={props.underline}
                handleChange={(propKey, value) => setProp(
                    (props) => (props[propKey] = value),
                )}
            />
        </div>
    );
}

Text.craft = {
    props: {
        bold: false,
        italic: false,
        underline: false,
        fontSize: 1,
        padding: [1, 1, 1, 1],
        text: 'Hello, World!',
    },
    related: {
        attributesPanel: TextAttributesPanel,
    },
};
