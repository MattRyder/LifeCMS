import React from 'react';
// import PropTypes from 'proptypes';
import { withRouter } from 'react-router';
import { Link } from 'react-router-dom';

import './MenuComponent.scss';

class MenuComponent extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            menuItems: this.props.menuItems,
        };

        this.getClassNames = this.getClassNames.bind(this);
    }

    getClassNames(menuItem) {
        const classNames = ["side-menu-item"];

        if (menuItem.link === this.props.location.pathname) {
            classNames.push("side-menu-item-active");
        }

        return classNames.join(" ");
    }

    render() {
        return (
            <ul className="side-menu">
                {this.state.menuItems.map((menuItem, i) => (
                    <Link key={i} to={menuItem.link}>
                        <li key={i} className={this.getClassNames(menuItem)}>
                            {menuItem.title}
                        </li>
                    </Link>
                ))}
            </ul>
        )
    }
}

// MenuComponent.propTypes = {

// };

// MenuComponent.defaultTypes = {

// };

export default withRouter(MenuComponent);