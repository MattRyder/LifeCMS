import React from 'react';
import { useTranslations, useUser, useStateSelector } from 'hooks';
import { useParams } from 'react-router';
import CampaignDetailsComponent from 'components/Campaign/CampaignDetailsComponent/CampaignDetailsComponent';
import DetailPage from 'components/Util/DetailPage/DetailPage';

export default function CampaignDetails() {
    const { t, TextTranslationKeys } = useTranslations();

    const { userId } = useUser();

    const { id } = useParams();

    const campaign = useStateSelector(userId, 'campaign', 'campaigns', id);

    return (
        <DetailPage
            title={t(TextTranslationKeys.campaignView.detail.pageTitle)}
        >
            <CampaignDetailsComponent campaign={campaign} />
        </DetailPage>
    );
}
