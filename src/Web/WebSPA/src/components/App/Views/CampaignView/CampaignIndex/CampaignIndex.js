import React from 'react';
import { useSelector } from 'react-redux';
import { findUserCampaigns } from 'redux/redux-orm/ORM';
import { useContentApi, useUser, useTranslations } from '../../../../../hooks';
import { fetchCampaigns } from '../../../../../redux/actions/CampaignActions';
import Table from '../../../../Util/Table/Table';
import CampaignRowComponent from './CampaignRowComponent';
import CampaignIndexIntro from './CampaignIndexIntro';
import ListView from '../../ListView';

function CampaignList({ collection }) {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <ListView
            title={t(TextTranslationKeys.campaignView.index.pageTitle)}
            ctaText={t(TextTranslationKeys.campaignView.index.createCampaign)}
            ctaLinkTo="/campaigns/new"
        >
            <Table
                headings={[
                    t(TextTranslationKeys.campaign.properties.name),
                    t(TextTranslationKeys.campaignView.index.scheduledFor),
                    t(TextTranslationKeys.campaignView.index.createdAt),
                    '',
                ]}
                rowComponent={CampaignRowComponent}
                collection={collection}
            />
        </ListView>
    );
}

export default function CampaignIndex() {
    const { accessToken, userId } = useUser();

    const userCampaigns = useSelector((state) => findUserCampaigns(state, userId));

    const hasCampaigns = userCampaigns.length > 0;

    useContentApi(
        () => fetchCampaigns(accessToken, userId),
        accessToken,
        userId,
    );

    return hasCampaigns
        ? <CampaignList collection={userCampaigns} />
        : <CampaignIndexIntro />;
}
