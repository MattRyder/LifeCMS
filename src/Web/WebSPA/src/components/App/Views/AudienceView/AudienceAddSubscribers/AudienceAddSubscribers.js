import React, { useState } from 'react';
import { useParams } from 'react-router';
import { useDispatch } from 'react-redux';
import { Button } from 'reactstrap';
import SubscriberUploader from 'components/Audiences/SubscriberUploader/SubscriberUploader';
import FormPage from 'components/Util/FormPage';
import { useTranslations, useUser } from 'hooks';
import { addSubscriber } from 'redux/actions/SubscriberActions';
import { css } from 'emotion';
import { push } from 'connected-react-router';

const styles = {
    submit: css`
        display: flex;
        justify-content: flex-end;
    `,
};

export default function AudienceAddSubscribers() {
    const { accessToken } = useUser();

    const { id } = useParams();

    const { t, TextTranslationKeys } = useTranslations();

    const [subscribers, setSubscribers] = useState({});

    const dispatch = useDispatch();

    const handleSave = () => {
        Object.values(subscribers).map((subscriber) => dispatch(
            addSubscriber(accessToken, {
                audience_id: id,
                email_address: subscriber.emailAddress,
                name: subscriber.name,
                consent_confirmed: false,
            }),
        ));

        dispatch(push(`/audiences/${id}`));
    };

    return (
        <FormPage title={t(TextTranslationKeys.audienceView.addSubscribers.pageTitle)}>
            <SubscriberUploader
                subscribers={subscribers}
                handleSubscriberChange={setSubscribers}
            />

            <div className={styles.submit}>
                <Button color="primary" onClick={handleSave}>
                    {t(TextTranslationKeys.common.save)}
                </Button>
            </div>

        </FormPage>
    );
}

AudienceAddSubscribers.propTypes = {};

AudienceAddSubscribers.defaultProps = {};
