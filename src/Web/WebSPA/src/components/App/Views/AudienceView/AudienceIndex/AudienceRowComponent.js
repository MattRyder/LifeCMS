import React from 'react';
import {
    Dropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
} from 'reactstrap';
import { Link } from 'react-router-dom';
import { formatTimestampDate } from 'components/Util/Date';
import { css } from 'emotion';
import theme from 'theme';
import { useDispatch } from 'react-redux';
import { deleteAudience } from 'redux/actions/AudienceActions';
import { FireConfirmAlert } from 'FireAlert';
import { useToggle, useTranslations, useUser } from '../../../../../hooks';
import Icon, { Icons } from '../../../Iconography/Icon';

const styles = {
    name: css`
        display: flex;
        flex-direction: column;
        align-items: flex-start;
    `,
};

function AudienceRowComponent({ id, name, createdAt }) {
    const { accessToken } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const [isDropdownOpened, toggleDropdown] = useToggle(false);

    const dispatch = useDispatch();

    const handleDeleteClick = () => FireConfirmAlert(
        () => dispatch(deleteAudience(accessToken, id)),
    );

    return (
        <tr key={id}>
            <td className={styles.name}>
                {name}
                <Link
                    className={theme.components.link}
                    to={`/audiences/${id}/add-subscribers`}
                >
                    {t(TextTranslationKeys.audienceView.index.addSubscribers)}
                </Link>
            </td>
            <td>{formatTimestampDate(createdAt)}</td>
            <td>
                <Dropdown isOpen={isDropdownOpened} toggle={toggleDropdown}>
                    <DropdownToggle nav className="link-subdued">
                        <Icon icon={Icons.cog} />
                    </DropdownToggle>
                    <DropdownMenu right>
                        <DropdownItem tag={Link} to={`/audiences/${id}`}>
                            {t(TextTranslationKeys.common.details)}
                        </DropdownItem>
                        <DropdownItem divider />
                        <DropdownItem onClick={handleDeleteClick} className="text-danger">
                            {t(TextTranslationKeys.common.delete)}
                        </DropdownItem>
                    </DropdownMenu>
                </Dropdown>
            </td>
        </tr>
    );
}

AudienceRowComponent.defaultProps = {
    id: '',
    name: '',
    createdAt: new Date(),
};

export default AudienceRowComponent;
