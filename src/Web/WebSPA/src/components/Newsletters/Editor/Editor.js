import React, { useEffect, useRef } from 'react';
import {
    Editor as CraftJSEditor, Frame, Element,
} from '@craftjs/core';
import { cx, css } from 'emotion';
import anime from 'animejs';
import ErrorBoundary from 'components/Util/ErrorBoundary';
import {
    Row, Text, Columns,
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
    `,
    canvas: css`
        flex: 1;
    `,
    page: css`
        box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05),
            0 2px 2px rgba(0, 0, 0, 0.05),
            0 4px 4px rgba(0, 0, 0, 0.05),
            0 6px 8px rgba(0, 0, 0, 0.05),
            0 8px 16px rgba(0, 0, 0, 0.05);
        background-color: white;
        margin: 1rem;
        min-height: 50%;
    `,
};

export default function Editor({
    designSource, onSave, title,
}) {
    const animeRef = useRef(null);

    const canvasRef = useRef(null);

    useEffect(() => {
        animeRef.current = anime.timeline({
            duration: 1000 * 0.35,
            easing: 'easeOutQuad',
        }).add({
            begin: () => {
                document.body.style.overflow = 'hidden';
            },
        }).add({
            targets: canvasRef.current,
            opacity: [0, 1],
            translateY: [999, 0],
        }).add({
            complete: () => {
                document.body.style.overflow = '';
            },
        }, '+=500');
    }, [animeRef, canvasRef]);

    return (
        <ErrorBoundary>
            <div className={cx(styles.editor)}>
                <CraftJSEditor
                    resolver={{
                        Columns,
                        Row,
                        Text,
                    }}
                >
                    <div className={cx(styles.designer)}>
                        <Topbar onSave={onSave} title={title} />

                        <div className={cx(styles.canvas)} ref={canvasRef}>
                            <Frame data={designSource}>
                                <Element canvas className={cx(styles.page)} />
                            </Frame>
                        </div>
                    </div>

                    <EditorMenu />
                </CraftJSEditor>
            </div>
        </ErrorBoundary>
    );
}
