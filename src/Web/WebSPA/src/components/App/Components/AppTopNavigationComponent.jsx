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
    Dropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
} from 'reactstrap';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import decodeToken from '../../../openid/Token';
import TextTranslationKeys from '../../../i18n/TextTranslationKeys';
import userManager from '../../../openid/UserManager';
import { performLogout } from '../../../redux/actions/LogoutActions';
import './AppTopNavigationComponent.scss';
import Icon, { Icons } from '../Iconography/Icon';

const mapStateToProps = ({ oidc }) => ({
    user: oidc.user,
});

const mapDispatchToProps = (dispatch) => ({
    dispatchPerformLogout: () => dispatch(performLogout()),
});

const AppTopNavigationComponent = ({ dispatchPerformLogout, t, user }) => {
    const [isMenuOpened, setMenuOpened] = useState(false);

    const [isDropdownOpened, setDropdownOpened] = useState(false);

    const toggleMenuOpened = () => setMenuOpened(!isMenuOpened);

    const toggleDropdownOpened = () => setDropdownOpened(!isDropdownOpened);

    const onLoginButtonClick = () => userManager.signinRedirect();

    const onLogoutButtonClick = () => dispatchPerformLogout();

    const getUserId = () => {
        if (user) {
            const { sub } = decodeToken(user.access_token);

            return sub;
        }

        return null;
    }

    const userId = getUserId();

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

                        <Dropdown isOpen={isDropdownOpened} toggle={toggleDropdownOpened}>
                            <DropdownToggle nav caret className="link-subdued" />
                            <DropdownMenu right>
                                <DropdownItem>
                                    {!user || user.expired
                                        ? (
                                            <NavLink href="#" onClick={onLoginButtonClick}>
                                                {t(TextTranslationKeys.menu.login)}
                                            </NavLink>
                                        )
                                        : (
                                            <NavLink href="#" onClick={onLogoutButtonClick}>
                                                {t(TextTranslationKeys.menu.logout)}
                                            </NavLink>
                                        )}
                                </DropdownItem>
                            </DropdownMenu>
                        </Dropdown>
                    </Nav>
                </Collapse>
            </Navbar>
        </Container>
    );
};

export default connect(mapStateToProps, mapDispatchToProps)(
    withTranslation()(AppTopNavigationComponent),
);
