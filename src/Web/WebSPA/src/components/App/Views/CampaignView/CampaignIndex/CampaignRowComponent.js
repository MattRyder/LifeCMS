/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { useState } from 'react';
import {
    Dropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
} from 'reactstrap';
import { format, parseISO } from 'date-fns';
import { Link } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { deleteCampaign } from 'redux/actions/CampaignActions';
import { useTranslations, useUser } from '../../../../../hooks';
import { FireConfirmAlert } from '../../../../../FireAlert';
import Icon, { Icons } from '../../../Iconography/Icon';

const DATE_FORMAT = 'E, MMMM dd hh:mm a';

function CampaignRowComponent({
    item: {
        id, name, scheduledDate, createdAt,
    },
}) {
    const dispatch = useDispatch();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const [isDropdownOpened, setDropdownOpened] = useState(false);

    const toggleDropdownOpened = () => setDropdownOpened(!isDropdownOpened);

    const dispatchDeleteCampaign = (campaignId) => dispatch(
        deleteCampaign(accessToken, userId, campaignId),
    );

    const onDeleteClick = () => FireConfirmAlert(
        () => dispatchDeleteCampaign(id),
    );

    return (
        <tr key={id}>
            <td>{name}</td>
            <td>{format(parseISO(scheduledDate), DATE_FORMAT)}</td>
            <td>{format(parseISO(createdAt), DATE_FORMAT)}</td>
            <td>
                <Link to={`/campaigns/${id}/details`}>
                    {t(TextTranslationKeys.common.details)}
                </Link>
            </td>
            <td>
                <Dropdown isOpen={isDropdownOpened} toggle={toggleDropdownOpened}>
                    <DropdownToggle nav className="link-subdued">
                        <Icon icon={Icons.cog} />
                    </DropdownToggle>
                    <DropdownMenu right>
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

CampaignRowComponent.defaultProps = {
    campaign: {},
};

export default CampaignRowComponent;
