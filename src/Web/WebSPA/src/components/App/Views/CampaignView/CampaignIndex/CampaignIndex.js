import React from 'react';
import { useSelector } from 'react-redux';
import { useContentApi, useUser, useTranslations } from '../../../../../hooks';
import { fetchCampaigns } from '../../../../../redux/actions/CampaignActions';
import TableComponent from '../../../../Util/Table/TableComponent';
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
            <TableComponent
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

    const campaignState = useSelector((state) => state.campaign[userId]);

    const hasCampaigns = campaignState.campaigns && campaignState.campaigns.length > 0;

    useContentApi(
        () => fetchCampaigns(accessToken, userId),
        accessToken,
        userId,
    );

    return hasCampaigns
        ? <CampaignList collection={campaignState && campaignState.campaigns} />
        : <CampaignIndexIntro />;
}
