import React from 'react';
import { useLocation } from 'react-router';
import { Nav, NavItem, NavLink } from 'reactstrap';
import PropTypes from 'prop-types';
import { makeReturnHref } from 'QueryString';


function InternalNavigation({ links }) {
    const { search } = useLocation();

    return (
        <Nav fill>
            {links.map(({ href, text }) => (
                <NavItem key={href}>
                    <NavLink href={makeReturnHref(search, href)}>
                        {text}
                    </NavLink>
                </NavItem>

            ))}
        </Nav>
    );
}

InternalNavigation.propTypes = {
    links: PropTypes.arrayOf(PropTypes.shape({
        href: PropTypes.string.isRequired,
        text: PropTypes.string.isRequired,
    })),
};

InternalNavigation.defaultProps = {
    links: [],
};

export default InternalNavigation;
