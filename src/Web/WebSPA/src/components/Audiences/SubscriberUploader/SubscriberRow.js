import React from 'react';
import PropTypes from 'prop-types';
import { Button } from 'reactstrap';
import Icon, { Icons } from 'components/App/Iconography/Icon';
import EditableText from 'components/Util/EditableText';
import { css } from 'emotion';
import { useTranslations } from 'hooks';

export const SubscriberRow = ({
    id, name, emailAddress, handleChange, handleDelete,
}) => (
    <tr>
        <td>
            <EditableText
                text={name}
                handleTextChange={(newName) => handleChange({ id, name: newName, emailAddress })}
            />
        </td>
        <td>
            <EditableText
                text={emailAddress}
                handleTextChange={(newEmail) => handleChange({ id, name, emailAddress: newEmail })}
            />
        </td>
        <td style={{ textAlign: 'right' }}>
            <Button
                className="text-danger"
                onClick={() => handleDelete(id)}
                color="link danger"
                size="sm"
            >
                <Icon icon={Icons.trash} />
            </Button>
        </td>
    </tr>
);

SubscriberRow.propTypes = {
    id: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
    emailAddress: PropTypes.string.isRequired,
    handleChange: PropTypes.func.isRequired,
    handleDelete: PropTypes.func.isRequired,
};

export const AddSubscriberRow = ({ handleClick }) => {
    const { t, TextTranslationKeys } = useTranslations();

    const styles = {
        button: css`
            float: right;
        `,
    };

    return (
        <tr>
            <td colSpan="100%">
                <Button
                    color="secondary"
                    className={styles.button}
                    onClick={handleClick}
                    size="sm"
                >
                    <Icon icon={Icons.plus} />
                    <span>
                        &nbsp;
                        {t(TextTranslationKeys.common.add)}
                    </span>
                </Button>
            </td>
        </tr>
    );
};

AddSubscriberRow.propTypes = {
    handleClick: PropTypes.func.isRequired,
};
