import React from 'react';
import { Nav, Button } from 'reactstrap';
import { useEditor } from '@craftjs/core';
import { Row, Text } from './Components';

import './Toolbox.scss';

const elements = [
    {
        component: <Row />,
        name: 'Row',
    },
    {
        component: <Text />,
        name: 'Free Text',
    },
];

export default function Toolbox() {
    const { connectors: { create } } = useEditor();

    return (
        <div className="toolbox">
            <Nav vertical>
                {
                    elements.map((element) => (
                        <div key={element.name} className="toolbox-item" ref={(ref) => create(ref, element.component)}>
                            <Button outline block color="primary">
                                {element.name}
                            </Button>
                        </div>
                    ))
                }
            </Nav>
        </div>
    );
}
