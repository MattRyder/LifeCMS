import React from 'react';
import { Nav, Button } from 'reactstrap';
import { useEditor } from '@craftjs/core';
import { Columns, Row, Text } from './Components';

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
    {
        component: <Columns />,
        name: 'Columns',
    },
];

export default function Toolbox() {
    const { connectors: { create } } = useEditor();

    return (
        <div className="toolbox">
            <Nav vertical>
                {
                    elements.map((element) => (
                        <Button key={element.name} outline block color="primary">
                            <div
                                className="toolbox-item"
                                ref={(ref) => create(ref, element.component)}
                            >
                                {element.name}
                            </div>
                        </Button>
                    ))
                }
            </Nav>
        </div>
    );
}
