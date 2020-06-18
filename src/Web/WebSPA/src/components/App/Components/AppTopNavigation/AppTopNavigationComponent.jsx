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
                <NavbarBrand href="/">
                    <Icon icon={Icons.logo} />
                </NavbarBrand>

                <NavbarToggler onClick={toggleMenuOpened} />

                <Collapse isOpen={isMenuOpened} navbar>
                    <Nav className="col-md-12 navbar-options" navbar>
                        {userId ? (
                            <NavItem>
                                <NavLink href={`/profile/${userId}`}>
                                    {t(TextTranslationKeys.menu.profile)}
                                </NavLink>
                            </NavItem>
                        ) : null}

                        <NavItem>
                            <NavLink href="/notifications" className="dimmer">
                                <Icon icon={Icons.notification} />
                            </NavLink>
                        </NavItem>

                        <NavItem>
                            <NavLink href="/messages" className="dimmer">
                                <Icon icon={Icons.message} />
                            </NavLink>
                        </NavItem>

                        <NavItem>
                            <NavLink href="/chat" className="dimmer">
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
