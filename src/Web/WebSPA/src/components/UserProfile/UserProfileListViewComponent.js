import React from 'react';
import PropTypes from 'prop-types';
import { Table } from 'reactstrap';
import { useRouteMatch } from 'react-router-dom';

import './UserProfileListViewComponent.scss';
import UserProfileListViewRowComponent from './UserProfileListViewRowComponent';

function UserProfileListViewComponent({ userProfiles }) {
    const { path } = useRouteMatch();

    return (
        <div className="user-profile-list-view-component">
            <Table>
                <caption className="a11y-visually-hidden">A list of user profiles</caption>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Occupation</th>
                        <th>Location</th>
                        <th label="actions" />
                    </tr>
                </thead>
                <tbody>
                    {
                        userProfiles.length > 0
                            ? userProfiles.map((userProfile) => (
                                <UserProfileListViewRowComponent
                                    key={userProfile.id}
                                    path={path}
                                    userProfile={userProfile}
                                />
                            ))
                            : null
                    }
                </tbody>
            </Table>
        </div>
    );
}

UserProfileListViewComponent.propTypes = {
    userProfiles: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.string,
            name: PropTypes.string,
            occupation: PropTypes.string,
            location: PropTypes.string,
        }),
    ),
};

UserProfileListViewComponent.defaultProps = {
    userProfiles: [],
};

export default UserProfileListViewComponent;
