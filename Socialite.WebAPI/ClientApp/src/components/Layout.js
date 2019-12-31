import React, { Component } from 'react';
import { Container, Col, Row } from 'reactstrap';

import './Layout.scss';

export class Layout extends Component {
  render() {
    return (
      <div className="layout-view">
        <Container fluid>
          <Row>
            <Col xs="12" sm={{size: 8, offset: 2}} md={{ size: 6, offset: 3 }}>
              {this.props.children}
            </Col>
          </Row>
        </Container>
      </div>
    );
  }
}
