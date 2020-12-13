import React, { useState } from 'react';
import {
    Dropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
} from 'reactstrap';
import { Link } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { deleteCampaign } from 'redux/actions/CampaignActions';
import { formatTimestampDate } from 'components/Util/Date';
import { useTranslations, useUser } from '../../../../../hooks';
import { FireConfirmAlert } from '../../../../../FireAlert';
import Icon, { Icons } from '../../../Iconography/Icon';

function CampaignRowComponent({
    item: {
        id, name, scheduledDate, createdAt,
    },
}) {
    const dispatch = useDispatch();

    const { accessToken } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const [isDropdownOpened, setDropdownOpened] = useState(false);

    const toggleDropdownOpened = () => setDropdownOpened(!isDropdownOpened);

    const dispatchDeleteCampaign = (campaignId) => dispatch(
        deleteCampaign(accessToken, campaignId),
    );

    const onDeleteClick = () => FireConfirmAlert(
        () => dispatchDeleteCampaign(id),
    );

    return (
        <tr key={id}>
            <td>{name}</td>
            <td>{formatTimestampDate(scheduledDate)}</td>
            <td>{formatTimestampDate(createdAt)}</td>
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
