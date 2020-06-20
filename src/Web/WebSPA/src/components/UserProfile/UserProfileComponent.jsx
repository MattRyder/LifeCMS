import React from 'react';

import './UserProfileComponent.scss';

export default function ({
    name, avatarImageUri, headerImageUri, occupation, location,
}) {
    return (
        <div className="user-profile-component">
            <div
                className="header"
                style={{ backgroundImage: `url('${headerImageUri}')` }}
            />
            <div className="avatar-container">

                <div
                    className="avatar"
                    style={{ backgroundImage: `url('${avatarImageUri}')` }}
                />
            </div>
            <div className="info">
                <p className="name">{name}</p>
                <div className="what-where">
                    <div>
                        <span>{occupation}</span>
                    </div>
                    <div>
                        <span>&bull;</span>
                    </div>
                    <div>
                        <span>{location}</span>
                    </div>
                </div>
            </div>
        </div>
    );
}
