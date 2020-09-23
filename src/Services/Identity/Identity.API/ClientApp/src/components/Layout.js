import React from 'react';
import { Container, Col, Row } from 'reactstrap';
import LanguageSelect from './Util/LanguageSelect/LanguageSelect';

import './Layout.scss';

export default function Layout({ children }) {
    const colProps = {
        xs: '12',
        sm: { size: 10, offset: 1 },
        md: { size: 8, offset: 2 },
        lg: { size: 6, offset: 3 },
    };
    return (
        <div className="layout-view">
            <Container fluid>
                <Row>
                    <Col {...colProps}>
                        {children}
                    </Col>
                </Row>
                <Row>
                    <Col {...colProps}>
                        <LanguageSelect />
                    </Col>
                </Row>
            </Container>
        </div>
    );
}
