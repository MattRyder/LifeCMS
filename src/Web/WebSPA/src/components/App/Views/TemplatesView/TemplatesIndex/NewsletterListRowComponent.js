/* eslint-disable jsx-a11y/anchor-is-valid */
import React from 'react';
import PropTypes from 'prop-types';
import {
    Dropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
} from 'reactstrap';
import { Link } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { deleteNewsletter } from 'redux/actions/NewsletterTemplateActions';
import { useToggle, useTranslations, useUser } from '../../../../../hooks';
import { FireConfirmAlert } from '../../../../../FireAlert';
import Icon, { Icons } from '../../../Iconography/Icon';

export default function NewsletterListRowComponent({
    id, name,
}) {
    const dispatch = useDispatch();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const [isDropdownOpened, toggleDropdownOpened] = useToggle(false);

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
                        <DropdownItem divider />
                        <DropdownItem
                            tag={Link}
                            to={`/templates/${id}/edit`}
                        >
                            {t(TextTranslationKeys.common.edit)}
                        </DropdownItem>
                        <DropdownItem
                            tag={Link}
                            to={`/templates/${id}/duplicate`}
                        >
                            {t(TextTranslationKeys.common.duplicate)}
                        </DropdownItem>
                        <DropdownItem divider />
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

NewsletterListRowComponent.propTypes = {
    item: PropTypes.shape({
        id: PropTypes.string.isRequired,
        name: PropTypes.string.isRequired,
    }).isRequired,
};
