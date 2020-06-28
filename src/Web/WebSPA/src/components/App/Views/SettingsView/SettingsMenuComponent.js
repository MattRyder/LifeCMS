import React from 'react';
import { ListGroup, ListGroupItem } from 'reactstrap';
import { Link } from 'react-router-dom';
import Icon from '../../Iconography/Icon';
import { useTranslations } from '../../../../hooks';

export default function ({ groups }) {
    const { t, TextTranslationKeys } = useTranslations();

    const getListGroupItem = ({ icon, href, text }) => (
        <Link className="text-dark" to={href} key={href}>
            <ListGroupItem>
                <Icon icon={icon} />
                {text}
            </ListGroupItem>
        </Link>
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
