import React from 'react';
import { useSelector } from 'react-redux';
import { findUserAudiences } from 'redux/redux-orm/ORM';
import { fetchAudiences } from 'redux/actions/AudienceActions';
import { useContentApi, useUser, useTranslations } from '../../../../../hooks';
import Table from '../../../../Util/Table/Table';
import AudienceRowComponent from './AudienceRowComponent';
import ListView from '../../ListView';
import AudienceIntro from './AudienceIntro';

function AudienceList({ collection }) {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <ListView
            title={t(TextTranslationKeys.audienceView.index.pageTitle)}
            ctaText={t(TextTranslationKeys.audienceView.index.createAudience)}
            ctaLinkTo="/audiences/new"
        >
            <Table
                headings={[
                    t(TextTranslationKeys.audience.properties.name),
                    t(TextTranslationKeys.common.createdAt),
                ]}
            >
                { collection && collection.map(({ id, name, createdAt }) => (
                    <AudienceRowComponent key={id} id={id} name={name} createdAt={createdAt} />
                ))}
            </Table>
        </ListView>
    );
}

export default function AudienceIndex() {
    const { accessToken, userId } = useUser();

    const userAudiences = useSelector((state) => findUserAudiences(state, userId));

    const hasAudiences = userAudiences.length > 0;

    useContentApi(
        () => fetchAudiences(accessToken),
        accessToken,
    );

    return hasAudiences
        ? <AudienceList collection={userAudiences} />
        : <AudienceIntro />;
}
