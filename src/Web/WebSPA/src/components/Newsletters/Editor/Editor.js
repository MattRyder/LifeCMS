import React from 'react';
import {
    Editor as CraftJSEditor, Frame, Element,
} from '@craftjs/core';
import { Row, Text } from './Components';
import Toolbox from './Toolbox';
import AttributesPanel from './AttributesPanel';
import Topbar from './Topbar';

export default function Editor({
    designSource, onSave, title,
}) {
    return (
        <div className="editor">
            <CraftJSEditor
                resolver={{
                    Row,
                    Text,
                }}
            >
                <Topbar title={title} onSave={onSave} />

                <div className="editor-designer">
                    <Toolbox />

                    <div className="canvas">
                        <Frame data={designSource}>
                            <Element canvas className="page" />
                        </Frame>
                    </div>

                    <AttributesPanel />
                </div>

            </CraftJSEditor>
        </div>
    );
}