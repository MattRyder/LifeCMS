import React from 'react';
import { ListGroup, ListGroupItem } from 'reactstrap';
import { Link, useLocation } from 'react-router-dom';
import Icon from '../../Iconography/Icon';

export default function ({ groups }) {
    const { pathname } = useLocation();

    const getListGroupItem = ({ icon, href, text }) => (
        <ListGroupItem href="#" to={href} key={href} active={pathname.includes(href)} tag={Link}>
            <Icon icon={icon} />
            <span>{text}</span>
        </ListGroupItem>
    );

    const getListGroup = (key, menuItems = []) => (
        <ListGroup key={key}>
            {menuItems.map((menuItem) => getListGroupItem(menuItem))}
        </ListGroup>
    );

    return (
        <div className="settings-menu-component">
            {groups.map((menuItemGroup, i) => getListGroup(i, menuItemGroup))}
        </div>
    );
}
