import React from 'react';
import {
    useNode, Element,
} from '@craftjs/core';
import { PaddingAttribute } from '../Attributes';
import ComponentWrapper from './ComponentWrapper';

export default function Row() {
    const {
        connectors: { connect, drag },
        isSelected,
        padding,
    } = useNode((node) => ({
        padding: node.data.props.padding,
        isSelected: node.events.selected,
    }));
    return (
        <div ref={(ref) => connect(drag(ref))}>
            <ComponentWrapper padding={padding} isSelected={isSelected}>
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
        padding: [1, 1, 1, 1],
    },
    related: {
        attributesPanel: RowAttributesPanel,
    },
};
