import React from 'react';
import { css, cx } from 'emotion';
import { useNode } from '@craftjs/core';
import useImageState from 'hooks/useImageState';
import { PaddingAttribute } from '../Attributes';
import ComponentWrapper from './ComponentWrapper';
import ColorAttribute from '../Attributes/ColorAttribute';
import ImageAttribute from '../Attributes/ImageAttribute';
import { ReactComponent as ImageIcon } from '../Toolbox/Icons/ImageIcon.svg';

const styles = {
    image: css`
        width: 100%;
        height: auto;
    `,
    containerDefault: {
        container: css`
            display: flex;
            flex-direction: column;
            align-items: center;
            background-color: #cdc0be;
            color: #565656;
            padding: 1rem;
            font-size: smaller;
        `,
        icon: css`
            max-width: 2rem;
            margin-bottom: 0.5rem;

            svg {
                fill: #565656;
                width: 100%;
                height: auto;
            }
        `,
    },
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

            {imageSource ? (
                <ComponentWrapper
                    backgroundColor={backgroundColor}
                    padding={padding}
                    isSelected={isSelected}
                >
                    <img
                        src={imageSource}
                        className={cx(styles.image)}
                        alt=""
                    />
                </ComponentWrapper>

            ) : (
                <ComponentWrapper
                    backgroundColor={backgroundColor}
                    padding={[0.25, 0.25, 0.25, 0.25]}
                    isSelected={isSelected}
                >
                    <div className={styles.containerDefault.container}>
                        <div className={styles.containerDefault.icon}>
                            <ImageIcon />
                        </div>
                        <p>No image selected.</p>
                        <p> Add an image from the Style tab.</p>
                    </div>
                </ComponentWrapper>

            )}
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
        padding: [0.1, 0.2, 0.1, 0.2],
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
