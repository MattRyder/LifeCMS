import React from 'react';
import { useTranslations, useUser } from 'hooks';
import { useParams } from 'react-router';
import CampaignDetailsComponent from 'components/Campaign/CampaignDetailsComponent/CampaignDetailsComponent';
import DetailPage from 'components/Util/DetailPage/DetailPage';
import { findUserCampaign } from 'redux/redux-orm/ORM';
import { useSelector } from 'react-redux';

export default function CampaignDetails() {
    const { t, TextTranslationKeys } = useTranslations();

    const { userId } = useUser();

    const { id } = useParams();

    const campaign = useSelector((state) => findUserCampaign(id)(state, userId));

    return (
        <DetailPage
            title={t(TextTranslationKeys.campaignView.detail.pageTitle)}
        >
            <CampaignDetailsComponent campaign={campaign} />
        </DetailPage>
    );
}
