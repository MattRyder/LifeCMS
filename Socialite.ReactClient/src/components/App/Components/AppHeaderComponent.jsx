import React, { Component } from 'react';

import {
    Collapse,
    Container,
    Nav,
    Navbar,
    NavbarBrand,
    NavItem,
    NavLink,
    NavbarToggler,
} from 'reactstrap';

import './AppHeaderComponent.scss';
import SocialiteLogo from 'assets/images/socialite-logo.svg';

export default class AppHeader extends Component {
    constructor(props) {
        super(props);

        this.toggle = this.toggle.bind(this);

        this.state = {
            isOpen: false,
        }
    }

    toggle() {
        this.setState((prevState) => ({ isOpen: !prevState.isOpen }));
    }

    render() {
        const { isOpen } = this.state;

        return (
            <Container fluid className="p-0">
                <Navbar className="navbar-socialite" expand="md">
                    <NavbarBrand href="/">
                        <img src={SocialiteLogo} alt="socialite logo" />
                    </NavbarBrand>

                    <NavbarToggler onClick={this.toggle} />

                    <Collapse isOpen={isOpen} navbar>
                        <Nav className="col-md-9" navbar>
                            <NavItem>
                                <NavLink href="/profile/1">Profile</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink href="/login">Login</NavLink>
                            </NavItem>
                        </Nav>
                    </Collapse>
                </Navbar>
            </Container>
        );
    }
}
