import React from 'react';
import UserProfileFormComponent from '../../../../../UserProfile/UserProfileForm/UserProfileFormComponent';

import './UserProfilePaneIndex.scss';

export default function () {
    return (
        <div className="user-profile-pane-new">
            <div className="user-profile-header">
                <span>Create a new Identity</span>
            </div>

            <UserProfileFormComponent />
        </div>
    );
}
