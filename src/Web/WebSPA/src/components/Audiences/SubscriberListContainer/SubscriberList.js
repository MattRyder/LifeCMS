import React from 'react';
import PropTypes from 'prop-types';
import SubscriberListItem from './SubscriberListItem';

export default function SubscriberList({ subscribers, index, perPage }) {
    return subscribers
        .slice(index, index + perPage)
        .map((subscriber) => (
            <SubscriberListItem
                key={subscriber.id}
                email={subscriber.emailAddress}
                id={subscriber.id}
                name={subscriber.name}
                subscribedAt={subscriber.subscribedAt}
                unsubscribedAt={subscriber.unsubscribedAt}
            />
        ));
}

SubscriberList.propTypes = {
    subscribers: PropTypes.arrayOf(
        PropTypes.shape(SubscriberListItem.propTypes),
    ),
    index: PropTypes.number.isRequired,
    perPage: PropTypes.number.isRequired,
};

SubscriberList.defaultProps = {
    subscribers: [],
};
