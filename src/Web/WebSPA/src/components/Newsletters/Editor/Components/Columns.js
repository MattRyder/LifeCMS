import React from 'react';
import { useNode, Element } from '@craftjs/core';
import { cx, css } from 'emotion';
import { PaddingAttribute, SingleSpinnerAttribute } from '../Attributes';
import ComponentWrapper from './ComponentWrapper';
import ColorAttribute from '../Attributes/ColorAttribute';

const styles = {
    columnElement: css`
        min-height: 5rem;
    `,
    componentWrapper: css`
        > div {
            display: flex;
            flex-direction: row;

            > div {
                border: 1px solid rgba(0, 0, 0, 0.55);
                flex: 1;
            }
        }
`,
};

export default function Columns() {
    const {
        connectors: { connect, drag },
        isSelected,
        props: {
            columnCount,
            backgroundColor,
            padding,
        },
    } = useNode((node) => ({
        props: node.data.props,
        isSelected: node.events.selected,
    }));

    return (
        <div
            className={cx(styles.componentWrapper)}
            ref={(ref) => connect(drag(ref))}
        >
            <ComponentWrapper
                backgroundColor={backgroundColor}
                padding={padding}
                isSelected={isSelected}
            >
                {Array.from(Array(columnCount).keys()).map((i) => (
                    <Element
                        canvas
                        className={cx(styles.columnElement)}
                        key={`row-item-${i}`}
                        id={`row-item-${i}`}
                        is="div"
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

            <SingleSpinnerAttribute
                title="Columns"
                min={2}
                max={4}
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
        backgroundColor: '#fff',
        padding: [1, 1, 1, 1],
        columnCount: 2,
    },
    related: {
        attributesPanel: ColumnsAttributesPanel,
    },
};
