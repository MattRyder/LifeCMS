import React from 'react';
import { useNode, Element } from '@craftjs/core';
import { PaddingAttribute, SingleSpinnerAttribute } from '../../Attributes';
import ComponentWrapper from '../ComponentWrapper';

import './Columns.scss';

export default function Columns({ columnCount = 1 }) {
    const {
        connectors: { connect, drag },
        isSelected,
        padding,
    } = useNode((node) => ({
        padding: node.data.props.padding,
        isSelected: node.events.selected,
    }));

    return (
        <div className="editor-columns-component" ref={(ref) => connect(drag(ref))}>
            <ComponentWrapper
                padding={padding}
                isSelected={isSelected}
            >
                {Array.from(Array(columnCount).keys()).map((i) => (
                    <Element
                        canvas
                        key={`row-item-${i}`}
                        id={`row-item-${i}`}
                        is="div"
                        style={{ minHeight: '5rem' }}
                    />
                ))}
            </ComponentWrapper>
        </div>
    );
}

function ColumnsAttributesPanel() {
    const {
        actions: { setProp },
        props,
    } = useNode((node) => ({
        props: node.data.props,
    }));

    return (
        <div className="columns-attributes-panel">
            <PaddingAttribute
                values={props.padding}
                handleChange={(paddingValues) => {
                    setProp(
                        (props) => (props.padding = paddingValues),
                    );
                }}
            />

            <SingleSpinnerAttribute
                title="Columns"
                min={0}
                max={3}
                value={props.columnCount}
                setValue={(columnCount) => {
                    setProp((props) => (props.columnCount = columnCount));
                }}
            />
        </div>
    );
}

Columns.craft = {
    props: {
        padding: [1, 1, 1, 1],
        columnCount: 1,
    },
    related: {
        attributesPanel: ColumnsAttributesPanel,
    },
};
