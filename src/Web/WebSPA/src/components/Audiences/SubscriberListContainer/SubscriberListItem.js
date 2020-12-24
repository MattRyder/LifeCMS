import React from 'react';
import PropTypes from 'prop-types';
import { css } from 'emotion';
import { formatTimestampDate } from 'components/Util/Date';
import theme from 'theme';
import { useTranslations } from 'hooks';

const styles = {
    subscriberListItem: css`
        display: flex;
        flex-direction: column;
        padding: 0.85rem;
        justify-content: center;
    `,
    dateRow: css`
        display: flex;
        flex-direction: row;
    `,
    mutedText: css`
        color: ${theme.colors.textMuted};
        font-size: small;
        margin-bottom: 0;
    `,
};

export default function SubscriberListItem({ email, subscribedAt, unsubscribedAt }) {
    const { t, TextTranslationKeys } = useTranslations();

    const SubscriptionStateText = () => {
        const stateText = () => {
            if (unsubscribedAt) {
                return `
                    ${t(TextTranslationKeys.subscriber.properties.unsubscribedAt)}:
                    ${formatTimestampDate(unsubscribedAt)}`;
            }

            if (subscribedAt) {
                return `
                    ${t(TextTranslationKeys.subscriber.properties.subscribedAt)}:
                    ${formatTimestampDate(subscribedAt)}`;
            }

            return t(TextTranslationKeys.audienceView.details.subscriberList.notConfirmedText);
        };

        return (
            <p className={styles.mutedText}>
                {stateText()}
            </p>
        );
    };

    return (
        <div className={styles.subscriberListItem}>
            <h6>{email}</h6>
            <div className={styles.subscriberListItem.dateRow}>
                <SubscriptionStateText />
            </div>
        </div>
    );
}

SubscriberListItem.propTypes = {
    email: PropTypes.string,
    subscribedAt: PropTypes.string,
    unsubscribedAt: PropTypes.string,
};

SubscriberListItem.defaultProps = {
    email: '',
    subscribedAt: '',
    unsubscribedAt: '',
};
