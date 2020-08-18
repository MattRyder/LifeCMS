import React from 'react';
import { useNode } from '@craftjs/core';
import { PaddingAttribute } from '../Attributes';

export default function Row({
    padding,
    children,
}) {
    const {
        connectors: { connect, drag },
        isSelected,
    } = useNode((node) => ({
        padding: node.data.props.padding,
        isSelected: node.events.selected,
    }));
    return (
        <div ref={(ref) => connect(drag(ref))}>
            <div
                className={`page-component ${isSelected ? 'is-selected' : ''}`}
                style={{
                    padding: `${padding[0]}rem ${padding[1]}rem ${padding[2]}rem ${padding[3]}rem`,
                }}
            >
                {children}
            </div>
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
            <div>
                <PaddingAttribute
                    values={props.padding}
                    handleChange={(paddingValues) => {
                        setProp(
                            (props) => (props.padding = paddingValues),
                        );
                    }}
                />
            </div>
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
