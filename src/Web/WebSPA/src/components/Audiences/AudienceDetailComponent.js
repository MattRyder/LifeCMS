import React from 'react';
import PropTypes from 'prop-types';
import { useTranslations, useUser } from 'hooks';
import DetailRow from 'components/Util/DetailPage/DetailRow';
import Meta from 'components/Util/DetailPage/Meta';
import Paper from 'components/Util/Paper';
import { formatTimestampDate } from 'components/Util/Date';
import { useSelector } from 'react-redux';
import { findAudienceSubscribers, findUserAudience } from 'redux/redux-orm/ORM';
import SubscriberListContainer from './SubscriberListContainer/SubscriberListContainer';

export default function AudienceDetailsComponent({ id }) {
    const { t, TextTranslationKeys } = useTranslations();

    const { userId } = useUser();

    const audience = useSelector((state) => findUserAudience(id)(state, userId));

    const subscribers = useSelector((state) => findAudienceSubscribers(state, id));

    return (
        <>
            <Paper>
                <DetailRow
                    label={t(TextTranslationKeys.audience.properties.name)}
                    value={<span>{audience.name}</span>}
                    linkTo={`/audiences/${id}/update-name`}
                    linkText={t(TextTranslationKeys.common.update)}
                />

                <SubscriberListContainer
                    audienceId={id}
                    subscribers={subscribers}
                />
            </Paper>
            <Meta keyValues={[
                {
                    label: t(TextTranslationKeys.common.createdAt),
                    value: formatTimestampDate(audience.createdAt),
                },
                {
                    label: t(TextTranslationKeys.common.updatedAt),
                    value: formatTimestampDate(audience.updatedAt),
                },
            ]}
            />
        </>
    );
}

AudienceDetailsComponent.propTypes = {
    id: PropTypes.string.isRequired,
};
