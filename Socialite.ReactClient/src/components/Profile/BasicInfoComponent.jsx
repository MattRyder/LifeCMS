import React from 'react';
import PropTypes from 'proptypes';
import { BrowserRouter, Link } from 'react-router-dom';
import {
    Button
} from 'reactstrap';

import './BasicInfoComponent.scss';

export class ActionMenuItem {
    constructor(title, link) {
        this.title = title;
        this.link = link;
    }
};

export class UserProfile {
    constructor(givenName, familyName, occupationTitle, employerName, avatarUrl) {
        this.givenName = givenName;
        this.familyName = familyName;
        this.occupationTitle = occupationTitle;
        this.employerName = employerName;
        this.avatarUrl = avatarUrl;
    }
}

class BasicInfoComponent extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            userProfile: this.props.userProfile,
            actionMenuItems: this.props.actionMenuItems
        };

        this.renderActionMenu = this.renderActionMenu.bind(this);
    }

    renderActionMenu() {
        return this.state.actionMenuItems.length > 0 ? (
            <div className="action-menu">
                <BrowserRouter>
                    {this.state.actionMenuItems.map((actionMenuItem, i) => (
                        <Link key={i} to={actionMenuItem.link}>
                            <Button color="link" size="sm">{actionMenuItem.title}</Button>
                        </Link>
                    ))}
                </BrowserRouter>
            </div>
        ) : null;
    }

    render() {
        return (
            <div className='basic-info'>
                <div className='info cover-image'>
                    <div className='image'>
                        <img src={this.state.userProfile.avatarUrl} alt="user" />
                    </div>
                    <div className='description'>
                        <div className='name'>
                            <p className="given-name">{this.state.userProfile.givenName}</p>
                            <p className='family-name'>{this.state.userProfile.familyName}</p>
                        </div>
                        <div className='occupation'>
                            <p className='name-text'>{this.state.userProfile.occupationTitle}</p>
                        </div>
                    </div>
                </div>
                {this.renderActionMenu()}
            </div>
        )
    }
}

BasicInfoComponent.propTypes = {
    userProfile: PropTypes.instanceOf(UserProfile),
    actionMenuItems: PropTypes.arrayOf(PropTypes.instanceOf(ActionMenuItem))
};

BasicInfoComponent.defaultTypes = {
    userProfile: null,
    actionMenuItems: []
};

export default BasicInfoComponent;