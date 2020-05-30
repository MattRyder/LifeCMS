import React from 'react';
import { withRouter } from 'react-router';
import { Link } from 'react-router-dom';

import './MenuComponent.scss';

class MenuComponent extends React.Component {
    constructor(props) {
        super(props);

        this.getClassNames = this.getClassNames.bind(this);
    }

    getClassNames(path) {
        const classNames = ['side-menu-item'];

        if (path === this.props.location.pathname) {
            classNames.push('side-menu-item-active');
        }

        return classNames.join(' ');
    }

    render() {
        const { menuItems = [] } = this.props;

        return (
            <ul className="menu-component">
                {menuItems.map(({text, icon, path}, i) => (
                    <Link key={i} to={path}>
                        <li key={i} className={this.getClassNames(path)}>
                            {icon}
                            <span>{text}</span>
                        </li>
                    </Link>
                ))}
            </ul>
        );
    }
}

// MenuComponent.propTypes = {

// };

// MenuComponent.defaultTypes = {

// };

export default withRouter(MenuComponent);
