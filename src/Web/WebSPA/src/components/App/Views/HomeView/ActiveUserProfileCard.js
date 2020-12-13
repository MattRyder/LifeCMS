import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import { useTranslations } from 'hooks';
import { Card, CardTitle, CardValue } from 'components/Util/DashboardCard';
import { Link } from 'react-router-dom';
import { Button } from 'reactstrap';

const styles = {
    link: css`
        padding: 0 !important;
    `,
};

export default function ActiveUserProfileCard({ className, name }) {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <Card className={className}>
            <CardTitle>{t(TextTranslationKeys.homeView.activeUserProfileTitle)}</CardTitle>
            <CardValue>{name}</CardValue>
            <Button
                color="link"
                className={cx(styles.link)}
                tag={Link}
                to="/user-profiles"
            >
                {t(TextTranslationKeys.homeView.activeUserProfileCta)}
            </Button>
        </Card>
    );
}

ActiveUserProfileCard.propTypes = {
    name: PropTypes.string,
    className: PropTypes.string,
};

ActiveUserProfileCard.defaultProps = {
    name: ' ',
    className: '',
};
