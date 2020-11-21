import React, { useState, useEffect } from 'react';
import { push } from 'connected-react-router';
import { push as Menu } from 'react-burger-menu';
import { useMediaQuery } from 'react-responsive';
import { useDispatch } from 'react-redux';
import { Button } from 'reactstrap';
import LanguageSelect from 'components/Util/LanguageSelect/LanguageSelect';
import { css, cx } from 'emotion';
import theme from 'theme';
import { darken } from 'polished';
import Icon, { Icons } from '../../Iconography/Icon';
import UserManager from '../../../../openid/UserManager';
import { useTranslations, useUser } from '../../../../hooks';
import { performLogout } from '../../../../redux/actions/LogoutActions';

const styles = {
    wrapper: css`
        overflow-y: auto;
    `,
    burgerMenu: css`
        .bm-burger-button {
            position: fixed;
            width: 36px;
            height: 30px;
            left: 20px;
            bottom: 24px;
        }
        
        /* Color/shape of burger icon bars */
        .bm-burger-bars {
            background: ${darken(0.25, theme.colors.main)};
        }
        
        /* Color/shape of burger icon bars on hover*/
        .bm-burger-bars-hover {
            background: ${theme.colors.main};
        }
        
        /* Position and sizing of clickable cross button */
        .bm-cross-button {
            height: 24px;
            width: 24px;
        }
        
        /* Color/shape of close button cross */
        .bm-cross {
            background: #bdc3c7;
        }
        
        /* Styling of overlay */
        .bm-overlay {
            background: rgba(0, 0, 0, 0.3);
        }
    `,
    menu: css`
        background-color: #fff;
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        flex: 1;
        min-width: 16.5rem;
        max-width: 16.5rem;
        order: -1;
    `,
    subMenu: css`
        padding-top: 2rem;
    `,
    header: css`
        overflow: hidden;
        padding: 0.25rem 0.75rem;
        text-overflow: ellipsis;
        text-transform: uppercase;
        white-space: nowrap;
        font-size: 0.75rem;
        font-weight: bold;
        color: ${theme.colors.mainMenuHeader};
    `,
    ul: css`
        list-style-type: none;
        margin: 0.05rem 0;
        padding: 0;
    `,
    li: css`
        display: flex;
    `,
    link: css`
        color: ${theme.colors.mainLink} !important;
    `,
    button: css`
        margin: 0.25rem 0.5rem;
        padding: 0.2rem 1rem;
        width: 100%;

        border-radius: 3.5px;
        text-decoration: none;
        text-align: left !important;

        color: ${theme.colors.mainLink} !important;
        
        svg {
            font-size: 0.85rem;
            margin-right: 0.5rem;
            color: ${theme.colors.mainLink}, 25%);
        }

        &:hover,
        &:active,
        &.active {
            background-color: ${theme.colors.mainMenuItemActiveBackground} !important;
            color: ${theme.colors.mainLink} !important;
            text-decoration: none;

            svg {
                background-color: ${theme.colors.mainMenuItemActiveBackground} !important;
                color: ${theme.colors.mainLink} !important;
            }
        }
    `,
};

const NavLink = ({ icon, text, onClick }) => (
    <li className={cx(styles.li)}>
        <Button className={cx(styles.button)} type="button" color="link" onClick={onClick}>
            <Icon icon={icon} />
            {text}
        </Button>
    </li>
);

export default function NavigationMenu({ pageWrapId, outerContainerId }) {
    const isTabletOrMobile = useMediaQuery({
        query: '(max-width: 1199px)',
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

    const MenuContent = () => (
        <>
            <div className={cx(styles.wrapper)}>
                <div className={cx(styles.subMenu)}>
                    <header className={cx(styles.header)}>
                        {t(TextTranslationKeys.navigationMenu.header.content)}
                    </header>
                    <ul className={cx(styles.ul)}>
                        <NavLink
                            icon={Icons.newspaper}
                            text={t(TextTranslationKeys.navigationMenu.item.posts)}
                            onClick={() => closeMenuAndNavigateTo('/content')}
                        />
                    </ul>
                </div>
                <div className={cx(styles.subMenu)}>
                    <header className={cx(styles.header)}>
                        {t(TextTranslationKeys.navigationMenu.header.newsletters)}
                    </header>
                    <ul className={cx(styles.ul)}>
                        <NavLink
                            icon={Icons.bullhorn}
                            text={t(TextTranslationKeys.navigationMenu.item.campaigns)}
                            onClick={() => closeMenuAndNavigateTo('/campaigns')}
                        />
                        <NavLink
                            icon={Icons.message}
                            text={t(TextTranslationKeys.navigationMenu.item.templates)}
                            onClick={() => closeMenuAndNavigateTo('/templates')}
                        />
                    </ul>
                </div>
                <div className={cx(styles.subMenu)}>
                    <header className={cx(styles.header)}>
                        {t(TextTranslationKeys.navigationMenu.header.settings)}
                    </header>
                    <ul className={cx(styles.ul)}>
                        <NavLink
                            icon={Icons.userEdit}
                            text={t(TextTranslationKeys.navigationMenu.item.userProfiles)}
                            onClick={() => closeMenuAndNavigateTo('/user-profiles')}
                        />
                    </ul>
                </div>
                <div className={cx(styles.subMenu)}>
                    <ul className={cx(styles.ul)}>
                        <li>
                            {
                                !userId ? (
                                    <Button
                                        color="Link"
                                        className={cx(styles.link)}
                                        onClick={onLoginClick}
                                    >
                                        {t(TextTranslationKeys.navigationMenu.item.login)}
                                    </Button>
                                ) : (
                                    <Button
                                        color="Link"
                                        className={cx(styles.link)}
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
            <LanguageSelect />
        </>
    );

    return isTabletOrMobile ? (
        <div className={cx(styles.burgerMenu)}>
            <Menu
                isOpen={menuOpen}
                disableCloseOnEsc={!isTabletOrMobile}
                disableAutoFocus
                onStateChange={({ isOpen }) => setMenuOpen(isOpen)}
                noOverlay={!isTabletOrMobile}
                className={cx(styles.menu)}
                pageWrapId={pageWrapId}
                outerContainerId={outerContainerId}
                width={350}
            >
                <MenuContent />
            </Menu>
        </div>
    ) : (
        <div className={cx(styles.menu)}>
            <MenuContent />
        </div>
    );
}
