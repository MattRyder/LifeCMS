import React from 'react';
import { ListGroup, ListGroupItem } from 'reactstrap';
import { NavLink } from 'react-router-dom';
import Icon, { Icons } from '../../Iconography/Icon';
import { useTranslations } from '../../../../hooks';

export default function ({ groups }) {
    const { t, TextTranslationKeys } = useTranslations();

    const getListGroupItem = ({ icon, href, text }) => (
        <NavLink className="text-dark" to={href} key={href}>
            <ListGroupItem>
                <Icon icon={icon} />
                {text}
            </ListGroupItem>
        </NavLink>
    );

    const getListGroup = (menuItems = []) => menuItems.map((menuItem, i) => (
        <ListGroup flush key={i}>
            {getListGroupItem(menuItem)}
        </ListGroup>
    ));

    return (
        <div className="settings-menu-component">
            <span className="menu-title">
                {t(TextTranslationKeys.common.settings)}
            </span>
            {groups.map((menuItemGroup) => getListGroup(menuItemGroup))}
        </div>
    );
}
