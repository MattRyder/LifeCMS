import React from 'react';
import {
    useNode, Element,
} from '@craftjs/core';
import { PaddingAttribute } from '../Attributes';
import ComponentWrapper from './ComponentWrapper';
import ColorAttribute from '../Attributes/ColorAttribute';

export default function Row() {
    const {
        connectors: { connect, drag },
        isSelected,
        props: {
            backgroundColor,
            padding,
        },
    } = useNode((node) => ({
        isSelected: node.events.selected,
        props: node.data.props,
    }));

    return (
        <div ref={(ref) => connect(drag(ref))}>
            <ComponentWrapper
                backgroundColor={backgroundColor}
                padding={padding}
                isSelected={isSelected}
            >
                <Element canvas id="row-item" is="div" />
            </ComponentWrapper>
        </div>
    );
}

function RowAttributesPanel() {
    const {
        actions: { setProp },
        props,
    } = useNode((node) => ({
        props: node.data.props,
    }));

    return (
        <div className="row-attributes-panel">
            <ColorAttribute
                color={props.backgroundColor}
                title="Background Colour"
                handleChange={(color) => {
                    setProp((props) => (props.backgroundColor = color));
                }}
            />

            <PaddingAttribute
                values={props.padding}
                handleChange={(paddingValues) => {
                    setProp(
                        (props) => (props.padding = paddingValues),
                    );
                }}
            />
        </div>
    );
}

Row.craft = {
    props: {
        backgroundColor: '#FFF',
        padding: [1, 1, 1, 1],
    },
    related: {
        attributesPanel: RowAttributesPanel,
    },
};
