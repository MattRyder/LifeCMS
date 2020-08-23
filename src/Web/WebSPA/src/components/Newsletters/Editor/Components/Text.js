import React from 'react';
import { useNode } from '@craftjs/core';
import { css } from 'emotion';
import { SingleSpinnerAttribute, PaddingAttribute } from '../Attributes';
import TypographicAttribute from '../Attributes/TypographicAttribute';
import ComponentWrapper from './ComponentWrapper';

export default function Text({
    bold, italic, underline, text,
}) {
    const {
        connectors: { connect, drag },
        isSelected,
        fontSize,
        padding,
    } = useNode((state) => ({
        isSelected: state.events.selected,
        padding: state.data.props.padding,
        fontSize: state.data.props.fontSize,
    }));

    const className = css({
        fontWeight: bold ? 'bold' : 'initial',
        fontStyle: italic ? 'italic' : 'initial',
        textDecoration: underline ? 'underline' : 'initial',
    });

    return (
        <div ref={(ref) => connect(drag(ref))}>
            <ComponentWrapper
                padding={padding}
                fontSize={fontSize}
                isSelected={isSelected}
            >
                <p className={className}>
                    {text}
                </p>
            </ComponentWrapper>
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
            <SingleSpinnerAttribute
                title="Font Size"
                min={1}
                max={5}
                value={props.fontSize}
                setValue={(value) => setProp(
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
                text={props.text}
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
        text: '',
    },
    related: {
        attributesPanel: TextAttributesPanel,
    },
};
