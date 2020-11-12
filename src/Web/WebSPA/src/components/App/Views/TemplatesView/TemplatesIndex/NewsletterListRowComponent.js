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
import { deleteNewsletter } from '../../../../../redux/actions/NewsletterTemplateActions';
import { useTranslations, useUser } from '../../../../../hooks';
import { FireConfirmAlert } from '../../../../../FireAlert';
import Icon, { Icons } from '../../../Iconography/Icon';

function NewsletterListRowComponent({
    item: {
        id, name,
    },
}) {
    const dispatch = useDispatch();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const [isDropdownOpened, setDropdownOpened] = useState(false);

    const toggleDropdownOpened = () => setDropdownOpened(!isDropdownOpened);

    const dispatchDeleteNewsletter = (newsletterId) => dispatch(
        deleteNewsletter(accessToken, userId, newsletterId),
    );

    const onDeleteClick = () => FireConfirmAlert(
        () => dispatchDeleteNewsletter(id),
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
                        <DropdownItem
                            tag={Link}
                            to={`/templates/${id}/preview`}
                        >
                            {t(TextTranslationKeys.newsletterView.listViewPreview)}
                        </DropdownItem>
                        <DropdownItem
                            tag={Link}
                            to={`/templates/${id}/edit`}
                        >
                            {t(TextTranslationKeys.common.edit)}
                        </DropdownItem>
                        <DropdownItem
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

NewsletterListRowComponent.defaultProps = {
    newsletter: {},
};

export default NewsletterListRowComponent;
