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
import { useTranslations, useUser, useConfirm } from '../../hooks';
import Icon, { Icons } from '../App/Iconography/Icon';
import { deleteUserProfile } from '../../redux/actions/UserProfileActions';

const confirmUserProfileDelete = (sweetalert, onConfirm) => sweetalert.fire({
    title: 'Are you sure?',
    text: "You won't be able to reverse this action.",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
}).then(onConfirm);

export default function UserProfileListViewRowComponent({
    path,
    userProfile: {
        id, name, occupation, location,
    },
}) {
    const dispatch = useDispatch();

    const { SweetAlert } = useConfirm();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const [isDropdownOpened, setDropdownOpened] = useState(false);

    const toggleDropdownOpened = () => setDropdownOpened(!isDropdownOpened);

    const dispatchDeleteUserProfile = (userProfileId) => dispatch(
        deleteUserProfile(accessToken, userId, userProfileId),
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
                        <DropdownItem>
                            <Link to={`${path}/${id}/edit`}>
                                {t(TextTranslationKeys.common.edit)}
                            </Link>
                        </DropdownItem>
                        <DropdownItem>
                            <a
                                href="#"
                                className="text-danger"
                                onClick={() => {
                                    confirmUserProfileDelete(
                                        SweetAlert, () => dispatchDeleteUserProfile(id),
                                    );
                                }}
                            >
                                {t(TextTranslationKeys.common.delete)}
                            </a>
                        </DropdownItem>
                    </DropdownMenu>
                </Dropdown>
            </td>
        </tr>
    );
}
