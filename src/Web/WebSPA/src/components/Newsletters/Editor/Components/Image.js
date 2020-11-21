import React from 'react';
import { css, cx } from 'emotion';
import { useNode } from '@craftjs/core';
import { PaddingAttribute } from '../Attributes';
import ComponentWrapper from './ComponentWrapper';
import ColorAttribute from '../Attributes/ColorAttribute';
import ImageAttribute from '../Attributes/ImageAttribute';

const styles = {
    image: css`
        width: 100%;
        height: auto;
    `,
};

export default function Image() {
    const {
        connectors: { connect, drag },
        isSelected,
        props: {
            backgroundColor,
            padding,
            file,
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
                <img
                    src={file && file.url}
                    className={cx(styles.image)}
                    alt=""
                />
            </ComponentWrapper>
        </div>
    );
}

function ImageAttributesPanel() {
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

            <ImageAttribute
                file={props.file}
                handleChange={(file) => {
                    setProp((props) => (props.file = {
                        name: file.name,
                        size: file.size,
                        contentType: file.type,
                        url: file.url,
                    }));
                }}
            />
        </div>
    );
}

Image.craft = {
    props: {
        backgroundColor: '#fff',
        padding: [1, 1, 1, 1],
        file: {},
    },
    related: {
        attributesPanel: ImageAttributesPanel,
    },
};
