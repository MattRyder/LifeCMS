import React from 'react';
import { useNode } from '@craftjs/core';
import { css } from 'emotion';
import { SingleSpinnerAttribute, PaddingAttribute } from '../Attributes';
import TypographicAttribute from '../Attributes/TypographicAttribute';
import ComponentWrapper from './ComponentWrapper';
import ColorAttribute from '../Attributes/ColorAttribute';

export default function Text({
    bold, italic, underline, text,
}) {
    const {
        connectors: { connect, drag },
        isSelected,
        props: {
            backgroundColor,
            color,
            fontSize,
            padding,
        },
    } = useNode((state) => ({
        isSelected: state.events.selected,
        props: state.data.props,
    }));

    const className = css({
        fontWeight: bold ? 'bold' : 'initial',
        fontStyle: italic ? 'italic' : 'initial',
        textDecoration: underline ? 'underline' : 'initial',
    });

    return (
        <div ref={(ref) => connect(drag(ref))}>
            <ComponentWrapper
                backgroundColor={backgroundColor}
                color={color}
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
        <div>
            <ColorAttribute
                color={props.backgroundColor}
                title="Background Colour"
                handleChange={(color) => {
                    setProp((props) => (props.backgroundColor = color));
                }}
            />
            <ColorAttribute
                color={props.color}
                title="Text Colour"
                handleChange={(color) => {
                    setProp((props) => (props.color = color));
                }}
            />
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
