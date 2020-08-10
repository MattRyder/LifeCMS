/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { useState } from 'react';
import {
    Dropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
} from 'reactstrap';
import { Link, useRouteMatch } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { deleteNewsletter } from '../../../../../redux/actions/NewsletterActions';
import { useTranslations, useUser, useConfirm } from '../../../../../hooks';
import Icon, { Icons } from '../../../Iconography/Icon';

const confirmNewsletterDelete = (sweetalert, onConfirm) => sweetalert.fire({
    title: 'Are you sure?',
    text: "You won't be able to reverse this action.",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
}).then(onConfirm);

function NewsletterListRowComponent({
    newsletter: {
        id, name,
    },
}) {
    const dispatch = useDispatch();

    const { SweetAlert } = useConfirm();

    const { accessToken, userId } = useUser();

    const { path } = useRouteMatch();

    const { t, TextTranslationKeys } = useTranslations();

    const [isDropdownOpened, setDropdownOpened] = useState(false);

    const toggleDropdownOpened = () => setDropdownOpened(!isDropdownOpened);

    const dispatchDeleteNewsletter = (newsletterId) => dispatch(
        deleteNewsletter(accessToken, userId, newsletterId),
    );

    return (
        <tr key={id}>
            <td>{name}</td>
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
                                    confirmNewsletterDelete(
                                        SweetAlert,
                                        () => dispatchDeleteNewsletter(id),
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

NewsletterListRowComponent.defaultProps = {
    newsletter: {},
};

export default NewsletterListRowComponent;
