import React, { useEffect, useRef, useState } from 'react';
import {
    Editor as CraftJSEditor, Frame, Element,
} from '@craftjs/core';
import { cx, css } from 'emotion';
import anime from 'animejs';
import ErrorBoundary from 'components/Util/ErrorBoundary';
import { boxShadow } from 'theme';
import { rgba } from 'polished';
import {
    Row, Text, Columns, Image,
} from './Components';
import Topbar from './Topbar';
import EditorMenu from './EditorMenu';

const styles = {
    editor: css`
        display: flex;
        flex: 1;
        flex-direction: row;
    `,
    designer: css`
        display: flex;
        flex-direction: column;
        flex: 0.75;
        height: 100%;
        overflow-y: auto;
    `,
    overflowHidden: css`
        overflow-y: hidden;
    `,
    canvas: css`
        flex: 1;
    `,
    frame: css`
        ${boxShadow(rgba(0, 0, 0, 0.05))}
        background-color: white;
        margin: 1rem;
    `,
    pageRoot: css`
        height: 100%;
    `,
};

export default function Editor({
    designSource, onSave, title,
}) {
    const animeRef = useRef(null);

    const canvasRef = useRef(null);

    const [isAnimating, setIsAnimating] = useState(true);

    useEffect(() => {
        animeRef.current = anime.timeline({
            duration: 1000 * 0.35,
            easing: 'easeOutQuad',
        }).add({
            begin: () => {
                setIsAnimating(true);
            },
        }).add({
            targets: canvasRef.current,
            opacity: [0, 1],
            translateY: [999, 0],
        }).add({
            complete: () => {
                setIsAnimating(false);
            },
        }, '+=500');
    }, [animeRef, canvasRef]);

    return (
        <ErrorBoundary>
            <div className={cx(styles.editor)}>
                <CraftJSEditor resolver={{
                    Columns, Row, Text, Image,
                }}
                >
                    <div className={cx(
                        styles.designer,
                        isAnimating && styles.overflowHidden,
                    )}
                    >
                        <Topbar onSave={onSave} title={title} />

                        <div className={cx(styles.canvas, styles.frame)} ref={canvasRef}>
                            <Frame data={designSource}>
                                <Element canvas className={cx(styles.pageRoot)} />
                            </Frame>
                        </div>
                    </div>

                    <EditorMenu />
                </CraftJSEditor>
            </div>
        </ErrorBoundary>
    );
}
