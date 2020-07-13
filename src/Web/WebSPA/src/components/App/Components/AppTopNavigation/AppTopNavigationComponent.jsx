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

const createNavItem = ({ children, to }) => (
    <NavItem>
        <NavLink tag={Link} to={to} className="dimmer">
            {children}
        </NavLink>
    </NavItem>
);

const navigationItems = ({ isMenuOpened, userId, t, TextTranslationKeys }) => [
    userId ? [
        createNavItem({
            children: t(TextTranslationKeys.menu.profile),
            to: `/profile/${userId}`,
        }),
        createNavItem({
            children: t(TextTranslationKeys.menu.newsletters),
            to: '/newsletters',
        }),
    ] : null,
    createNavItem({
        children: isMenuOpened ? <span>Notifications</span> : <Icon icon={Icons.notification} />,
        to: '/notifications',
    }),
    createNavItem({
        children: isMenuOpened ? <span>Settings</span> : <Icon icon={Icons.settings} />,
        to: '/settings',
    }),
];

export default function () {
    const [isMenuOpened, setMenuOpened] = useState(false);

    const toggleMenuOpened = () => setMenuOpened(!isMenuOpened);

    const { userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    return (
        <Container fluid className="p-0" role="tablist">
            <Navbar className="navbar-application" expand="md">
                <NavbarBrand tag={Link} to="/" tabIndex="0">
                    <Icon icon={Icons.logo} />
                </NavbarBrand>

                <NavbarToggler onClick={toggleMenuOpened} />

                <Collapse isOpen={isMenuOpened} navbar>
                    <Nav className="col-md-12 navbar-options" navbar>
                        {navigationItems({
                            isMenuOpened, userId, t, TextTranslationKeys,
                        })}
                        <Dropdown />
                    </Nav>
                </Collapse>
            </Navbar>
        </Container>
    );
}
