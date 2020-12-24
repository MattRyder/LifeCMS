import React from 'react';
import PropTypes from 'prop-types';
import { v4 as uuidv4 } from 'uuid';
import Papa from 'papaparse';
import { cx, css } from 'emotion';
import { useToggle, useTranslations } from 'hooks';
import { Button } from 'reactstrap';
import { rgba } from 'polished';
import Table from 'components/Util/Table/Table';
import FileModal from 'components/Newsletters/Editor/Components/FileModal';
import { AddSubscriberRow, SubscriberRow } from './SubscriberRow';

const styles = {
    subscriberUploader: css`
        padding: 1rem 0 !important;
    `,
    titleRow: css`
        display: flex;
        align-items: center;
        justify-content: space-between;
    `,
    subscriberTable: css`
        border: 1px solid ${rgba(0, 0, 0, 0.066)};
        margin-top: 0.5rem;
        margin-bottom: 0.5rem;
    `,
    csvTemplateLink: css`
        padding: 0 1rem;
    `,
};

export default function SubscriberUploader({ subscribers, handleSubscriberChange }) {
    const { t, TextTranslationKeys } = useTranslations();

    const [isModalOpen, toggleModalOpen] = useToggle();

    const handleCsvUpload = (file) => {
        Papa.parse(file, {
            header: true,
            skipEmptyLines: true,
            complete: (result) => {
                const records = {};

                result.data.forEach(({ name, email_address: emailAddress }) => {
                    const id = uuidv4();

                    records[id] = {
                        name,
                        emailAddress,
                    };
                });

                handleSubscriberChange(records);
            },
        });
    };

    const addSubscriberRecord = () => {
        const id = uuidv4();

        handleSubscriberChange({
            [id]: { name: '', emailAddress: '' },
            ...subscribers,
        });
    };

    return (
        <div className={cx(styles.subscriberUploader)}>
            <div className={cx(styles.titleRow)}>
                <Button
                    color="link"
                    size="sm"
                    target="_blank"
                    href="/files/audience_subscriber_upload_template.csv"
                >
                    {t(TextTranslationKeys.audienceView.create.subscriberUploader.downloadCSV)}
                </Button>
                <Button
                    color="primary"
                    size="sm"
                    onClick={toggleModalOpen}
                >
                    {t(TextTranslationKeys.audienceView.create.subscriberUploader.importCSV)}
                </Button>
            </div>
            <Table
                className={styles.subscriberTable}
                headings={[
                    t(TextTranslationKeys.subscriber.properties.name),
                    t(TextTranslationKeys.subscriber.properties.emailAddress),
                ]}
                accessibilityDescription=""
            >
                {
                    Object.entries(subscribers).reverse().map((item) => {
                        const [key, subscriber] = item;

                        return (
                            <SubscriberRow
                                key={key}
                                id={key}
                                name={subscriber.name}
                                emailAddress={subscriber.emailAddress}
                                handleChange={({ id, name, emailAddress }) => {
                                    handleSubscriberChange({
                                        ...subscribers,
                                        [id]: { name, emailAddress },
                                    });
                                }}
                                handleDelete={(id) => {
                                    const { [id]: deletedItem, ...rest } = subscribers;

                                    handleSubscriberChange(rest);
                                }}
                            />
                        );
                    })
                }
                <AddSubscriberRow handleClick={addSubscriberRecord} />
            </Table>

            <FileModal
                acceptedTypes="text/csv"
                isOpen={isModalOpen}
                maxFiles={1}
                setAcceptedFiles={(files) => handleCsvUpload(files[0])}
                title={t(TextTranslationKeys.audienceView.create.subscriberUploader.fileModalTitle)}
                toggleIsOpen={toggleModalOpen}
            />
        </div>
    );
}

SubscriberUploader.propTypes = {
    subscribers: PropTypes.objectOf(PropTypes.shape({
        name: PropTypes.string,
        emailAddress: PropTypes.string,
    })),
    handleSubscriberChange: PropTypes.func.isRequired,
};

SubscriberUploader.defaultProps = {
    subscribers: {},
};
