import React from 'react';
import { useContentApi, useTranslations, useUser } from 'hooks';
import { useParams } from 'react-router';
import AudienceDetailsComponent from 'components/Audiences/AudienceDetailComponent';
import DetailPage from 'components/Util/DetailPage/DetailPage';
import { fetchAudiences } from 'redux/actions/AudienceActions';
import { fetchSubscribers } from 'redux/actions/SubscriberActions';

export default function AudienceDetails() {
    const { t, TextTranslationKeys } = useTranslations();

    const { accessToken } = useUser();

    const { id } = useParams();

    useContentApi(
        () => fetchAudiences(accessToken),
        accessToken,
    );

    useContentApi(
        () => fetchSubscribers(accessToken, id),
        accessToken,
    );

    return (
        <DetailPage title={t(TextTranslationKeys.audienceView.details.pageTitle)}>
            <AudienceDetailsComponent id={id} />
        </DetailPage>
    );
}
