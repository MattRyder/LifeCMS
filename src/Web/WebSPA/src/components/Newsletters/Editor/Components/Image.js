import React from 'react';
import { css, cx } from 'emotion';
import { useNode } from '@craftjs/core';
import useImageState from 'hooks/useImageState';
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
            newFile,
            urn,
        },
    } = useNode((node) => ({
        isSelected: node.events.selected,
        props: node.data.props,
    }));

    const existingImage = useImageState({ urn });

    const imageSource = newFile.url || existingImage.uri;

    return (
        <div ref={(ref) => connect(drag(ref))}>
            <ComponentWrapper
                backgroundColor={backgroundColor}
                padding={padding}
                isSelected={isSelected}
            >
                {imageSource && (
                    <img
                        src={imageSource}
                        className={cx(styles.image)}
                        alt=""
                    />
                )}
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
                urn={props.urn}
                previewImageUrl={props.newFile.url}
                handleFileChange={(file) => {
                    const newFile = {
                        name: file.name,
                        size: file.size,
                        contentType: file.type,
                        url: file.url,
                    };

                    setProp((props) => (props.newFile = newFile));
                }}
            />
        </div>
    );
}

Image.craft = {
    props: {
        backgroundColor: '#fff',
        padding: [1, 1, 1, 1],
        urn: null,
        newFile: {
            name: '',
            size: 0,
            contentType: '',
            url: null,
        },
    },
    related: {
        attributesPanel: ImageAttributesPanel,
    },
};
