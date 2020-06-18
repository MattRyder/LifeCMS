import React, { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import {
    NavLink,
    Dropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
} from 'reactstrap';
import { useTranslations } from '../../../../hooks'
import userManager from '../../../../openid/UserManager';
import { performLogout } from '../../../../redux/actions/LogoutActions';

export default function () {
    const user = useSelector((state) => state.oidc.user);

    const dispatch = useDispatch();

    const { t, TextTranslationKeys } = useTranslations();

    const [isDropdownOpened, setDropdownOpened] = useState(false);

    const toggleDropdownOpened = () => setDropdownOpened(!isDropdownOpened);

    const onLoginButtonClick = () => userManager.signinRedirect();

    const onLogoutButtonClick = () => dispatch(performLogout());

    return (
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
    );
}
