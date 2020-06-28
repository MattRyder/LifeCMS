import React from 'react';
import PropTypes from 'prop-types';

import './UserProfileComponent.scss';
import UserInfoComponent from './UserInfoComponent';
import UserAvatarComponent from './UserAvatarComponent';

function UserProfileComponent({
    name, avatarImageUri, headerImageUri, occupation, location, showHeader,
}) {
    const getHeader = () => (
        <div
            className="header"
            style={{ backgroundImage: `url('${headerImageUri}')` }}
        />
    );

    return (
        <div className="user-profile-component" role="tabpanel">
            {showHeader ? getHeader() : null}

            <UserAvatarComponent avatarImageUri={avatarImageUri} />

            <UserInfoComponent
                name={name}
                occupation={occupation}
                location={location}
            />
        </div>
    );
}

UserProfileComponent.propTypes = {
    name: PropTypes.string,
    avatarImageUri: PropTypes.string,
    headerImageUri: PropTypes.string,
    occupation: PropTypes.string,
    location: PropTypes.string,
    showHeader: PropTypes.bool,
};

UserProfileComponent.defaultProps = {
    name: '',
    avatarImageUri: '',
    headerImageUri: '',
    occupation: '',
    location: '',
    showHeader: true,
};

export default UserProfileComponent;
