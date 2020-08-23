import React from 'react';
import { ListGroup, ListGroupItem } from 'reactstrap';
import { Link } from 'react-router-dom';
import Icon from '../../App/Iconography/Icon';

import './ListGroupComponent.scss';

const makeListGroupItem = ({ to, icon, text }) => (
    <ListGroupItem
        key={to}
        to={to}
        tag={Link}
        active={window.location.pathname === to}
    >
        <Icon icon={icon} />
        <span>{text}</span>
    </ListGroupItem>
);

function ListGroupComponent({ items }) {
    return (
        <ListGroup className="list-group-component">
            {
                items.map((item) => makeListGroupItem(item))
            }
        </ListGroup>
    );
}

ListGroupComponent.defaultProps = {
    items: [],
};

export default ListGroupComponent;
