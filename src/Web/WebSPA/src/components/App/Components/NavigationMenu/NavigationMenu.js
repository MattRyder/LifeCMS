import React, { useState, useEffect } from 'react';
import { push } from 'connected-react-router';
import { push as Menu } from 'react-burger-menu';
import { useMediaQuery } from 'react-responsive';
import { useDispatch } from 'react-redux';
import { Button } from 'reactstrap';
import Icon, { Icons } from '../../Iconography/Icon';
import UserManager from '../../../../openid/UserManager';
import { useTranslations, useUser } from '../../../../hooks';

import './NavigationMenu.scss';
import { performLogout } from '../../../../redux/actions/LogoutActions';

const createNavLink = (icon, text, onClick) => (
    <li>
        <Button type="button" color="link" onClick={onClick}>
            <Icon icon={icon} />
            {text}
        </Button>
    </li>
);

export default function NavigationMenu({ pageWrapId, outerContainerId }) {
    const isTabletOrMobile = useMediaQuery({
        query: '(max-width: 1200px)',
    });

    const dispatch = useDispatch();

    const { userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const [menuOpen, setMenuOpen] = useState(!isTabletOrMobile);

    const closeMenuAndNavigateTo = (to) => {
        setMenuOpen(false);

        dispatch(push(to));
    };

    useEffect(() => {
        setMenuOpen(!isTabletOrMobile);
    }, [isTabletOrMobile]);

    const onLoginClick = () => UserManager.signinRedirect();

    const onLogoutClick = () => dispatch(performLogout());

    const menuContent = (
        <div>
            <div className="sub-menu">
                <header>{t(TextTranslationKeys.navigationMenu.header.content)}</header>
                <ul>
                    {createNavLink(
                        Icons.newspaper,
                        t(TextTranslationKeys.navigationMenu.item.posts),
                        () => closeMenuAndNavigateTo('/content'),
                    )}
                </ul>
            </div>
            <div className="sub-menu">
                <header>{t(TextTranslationKeys.navigationMenu.header.newsletters)}</header>
                <ul>
                    {createNavLink(
                        Icons.bullhorn,
                        t(TextTranslationKeys.navigationMenu.item.campaigns),
                        () => closeMenuAndNavigateTo('/campaigns'),
                    )}
                    {createNavLink(
                        Icons.message,
                        t(TextTranslationKeys.navigationMenu.item.templates),
                        () => closeMenuAndNavigateTo('/newsletter/templates'),
                    )}
                </ul>
            </div>
            <div className="sub-menu">
                <header>{t(TextTranslationKeys.navigationMenu.header.settings)}</header>
                <ul>
                    {createNavLink(
                        Icons.userEdit,
                        t(TextTranslationKeys.navigationMenu.item.userProfiles),
                        () => closeMenuAndNavigateTo('/settings/user-profiles'),
                    )}
                </ul>
            </div>
            <div className="sub-menu">
                <ul>
                    <li>
                        {
                            !userId ? (
                                <Button
                                    color="Link"
                                    onClick={onLoginClick}
                                >
                                    {t(TextTranslationKeys.navigationMenu.item.login)}
                                </Button>
                            ) : (
                                <Button
                                    color="Link"
                                    onClick={onLogoutClick}
                                >
                                    {t(TextTranslationKeys.navigationMenu.item.logout)}
                                </Button>
                            )
                        }

                    </li>
                </ul>
            </div>
        </div>
    );

    return isTabletOrMobile ? (
        <Menu
            isOpen={menuOpen}
            disableCloseOnEsc={!isTabletOrMobile}
            disableAutoFocus
            onStateChange={({ isOpen }) => setMenuOpen(isOpen)}
            noOverlay={!isTabletOrMobile}
            className="navigation-menu"
            pageWrapId={pageWrapId}
            outerContainerId={outerContainerId}
            width={350}
        >
            {menuContent}
        </Menu>

    ) : (
        <div className="navigation-menu">
            {menuContent}
        </div>
    );
}
