import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { useTranslations } from 'hooks';
import { Button, ButtonGroup } from 'reactstrap';
import { css, cx } from 'emotion';
import theme from 'theme';
import { lighten } from 'polished';
import { Link } from 'react-router-dom';
import SubscriberList from './SubscriberList';
import NoSubscribersNotice from './NoSubscribersNotice';

export default function SubscriberListContainer({ audienceId, subscribers, perPage }) {
    const styles = {
        subscriberList: css`
            border: 1px solid ${lighten(0.35, theme.colors.tableBorder)};
            border-radius: 5px;
        `,
        header: css`
            align-items: center;
            border-bottom: 1px solid ${lighten(0.45, theme.colors.tableBorder)};
            background-color: ${theme.colors.subscriberListHeaderBackground};
            display: flex;
            justify-content: space-between;
            border-top-left-radius 5px;
            border-top-right-radius 5px;
        `,
        addSubscribersLink: css`
            margin: 1rem;
        `,
        subscriberListInner: css`
            & > div {
                border: 1px solid ${lighten(0.50, theme.colors.tableBorder)};
                border-left: none;
                border-right: none;

                :first-child {
                    border-top: none;
                }

                :last-child {
                    border-bottom: none;
                }
            }
        `,
        actionsContainer: {
            self: css`
                display: flex;
                justify-content: space-between;
                padding: 1rem;
                border-top: 1px solid ${lighten(0.45, theme.colors.tableBorder)};
            `,
            pageText: css`
                display: flex;
                align-items: center;
            `,
        },
        title: css`
            margin: 1rem;
        `,
    };

    const { t, TextTranslationKeys } = useTranslations();

    const [page, setPage] = useState(0);

    const pagination = {
        index: page * perPage,
        totalPages: Math.round(subscribers.length / perPage),
        willDisplay: subscribers.length > perPage,
    };

    return (
        <div className={styles.subscriberList}>
            <div className={styles.header}>
                <h4 className={styles.title}>
                    {t(TextTranslationKeys.audienceView.details.subscriberList.title)}
                </h4>
                <Link
                    className={cx(theme.components.link, styles.addSubscribersLink)}
                    to={`/audiences/${audienceId}/add-subscribers`}
                >
                    {t(TextTranslationKeys.audienceView.index.addSubscribers)}
                </Link>
            </div>
            <div className={styles.subscriberListInner}>
                { subscribers && subscribers.length > 0
                    ? (
                        <SubscriberList
                            subscribers={subscribers}
                            index={pagination.index}
                            perPage={perPage}
                        />
                    ) : <NoSubscribersNotice /> }
            </div>
            <div className={styles.actionsContainer.self}>
                <span className={styles.actionsContainer.pageText}>
                    {t(TextTranslationKeys.audienceView.details.subscriberList.pageText)}
                    &nbsp;
                    1 / 1
                </span>
                { pagination.willDisplay && (
                    <ButtonGroup>
                        <Button
                            disabled={page === 0}
                            onClick={() => setPage((page - 1) % pagination.totalPages)}
                            color="secondary"
                        >
                            {t(TextTranslationKeys.audienceView.details.subscriberList.previousPageButton)}
                        </Button>
                        <Button
                            disabled={page === pagination.totalPages - 1}
                            onClick={() => setPage((page + 1) % pagination.totalPages)}
                            color="secondary"
                        >
                            {t(TextTranslationKeys.audienceView.details.subscriberList.nextPageButton)}
                        </Button>
                    </ButtonGroup>
                )}
            </div>

        </div>
    );
}

SubscriberListContainer.propTypes = {
    subscribers: PropTypes.arrayOf(PropTypes.shape({
        emailAddress: PropTypes.string,
        id: PropTypes.string,
        name: PropTypes.string,
        subscribedAt: PropTypes.string,
        unsubscribedAt: PropTypes.string,
    })),
    perPage: PropTypes.number,
    audienceId: PropTypes.string.isRequired,
};

SubscriberListContainer.defaultProps = {
    subscribers: [],
    perPage: 10,
};
