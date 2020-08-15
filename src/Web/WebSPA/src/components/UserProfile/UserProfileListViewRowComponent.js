/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { useState } from 'react';
import {
    Dropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
} from 'reactstrap';
import { Link } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { useTranslations, useUser } from '../../hooks';
import Icon, { Icons } from '../App/Iconography/Icon';
import { FireConfirmAlert } from '../../FireAlert';
import { deleteUserProfile } from '../../redux/actions/UserProfileActions';

export default function UserProfileListViewRowComponent({
    path,
    userProfile: {
        id, name, occupation, location,
    },
}) {
    const dispatch = useDispatch();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const [isDropdownOpened, setDropdownOpened] = useState(false);

    const toggleDropdownOpened = () => setDropdownOpened(!isDropdownOpened);

    const dispatchDeleteUserProfile = (userProfileId) => dispatch(
        deleteUserProfile(accessToken, userId, userProfileId),
    );

    const onDeleteClick = () => FireConfirmAlert(
        () => dispatchDeleteUserProfile(id),
    );

    return (
        <tr key={id}>
            <td>{name}</td>
            <td>{occupation}</td>
            <td>{location}</td>
            <td>
                <Dropdown isOpen={isDropdownOpened} toggle={toggleDropdownOpened}>
                    <DropdownToggle nav className="link-subdued">
                        <Icon icon={Icons.cog} />
                    </DropdownToggle>
                    <DropdownMenu right>
                        <DropdownItem
                            tag={Link}
                            to={`${path}/${id}/edit`}
                        >
                            {t(TextTranslationKeys.common.edit)}
                        </DropdownItem>
                        <DropdownItem
                            href="#"
                            className="text-danger"
                            onClick={onDeleteClick}
                        >
                            {t(TextTranslationKeys.common.delete)}
                        </DropdownItem>
                    </DropdownMenu>
                </Dropdown>
            </td>
        </tr>
    );
}
