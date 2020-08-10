import React from 'react';
import { Route, Switch } from 'react-router';
import { Container, Col, Row } from 'reactstrap';
import { CallbackComponent, ErrorForm, LogoutComponent } from '../../Session';

import './SessionView.scss';

export default function SessionLayout() {
    return (
        <div className="layout-view">
            <Container fluid>
                <Row>
                    <Col xs="12" sm={{ size: 8, offset: 2 }} md={{ size: 6, offset: 3 }}>
                        <Switch>
                            <Route path="/session/logout" component={LogoutComponent} />
                            <Route path="/session/oauth_error" component={ErrorForm} />
                            <Route path="/session/oauth_callback" component={CallbackComponent} />
                        </Switch>
                    </Col>
                </Row>
            </Container>
        </div>
    );
}
