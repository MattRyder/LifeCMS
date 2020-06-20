import React, { useState } from 'react';
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
import { Link } from 'react-router-dom';
import { useUser, useTranslations } from '../../../../hooks';
import Dropdown from './Dropdown';
import Icon, { Icons } from '../../Iconography/Icon';

import './AppTopNavigationComponent.scss';

export default function () {
    const [isMenuOpened, setMenuOpened] = useState(false);

    const toggleMenuOpened = () => setMenuOpened(!isMenuOpened);

    const { userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    return (
        <Container fluid className="p-0">
            <Navbar className="navbar-application" expand="md">
                <NavbarBrand tag={Link} to="/">
                    <Icon icon={Icons.logo} />
                </NavbarBrand>

                <NavbarToggler onClick={toggleMenuOpened} />

                <Collapse isOpen={isMenuOpened} navbar>
                    <Nav className="col-md-12 navbar-options" navbar>
                        {userId ? (
                            <NavItem>
                                <NavLink tag={Link} to={`/profile/${userId}`}>
                                    {t(TextTranslationKeys.menu.profile)}
                                </NavLink>
                            </NavItem>
                        ) : null}

                        <NavItem>
                            <NavLink tag={Link} to="/notifications" className="dimmer">
                                <Icon icon={Icons.notification} />
                            </NavLink>
                        </NavItem>

                        <NavItem>
                            <NavLink tag={Link} to="/messages" href="#" className="dimmer">
                                <Icon icon={Icons.message} />
                            </NavLink>
                        </NavItem>

                        <NavItem>
                            <NavLink tag={Link} to="/chat" className="dimmer">
                                <Icon icon={Icons.chat} />
                            </NavLink>
                        </NavItem>

                        <Dropdown />
                    </Nav>
                </Collapse>
            </Navbar>
        </Container>
    );
}
