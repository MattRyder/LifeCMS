import React from 'react';
import { useTranslations, useUser, useStateSelector } from 'hooks';
import { useParams } from 'react-router';
import ViewNavigationBar from 'components/App/Components/ViewNavigationBar/ViewNavigationBar';
import CampaignDetailsComponent from 'components/Campaign/CampaignDetailsComponent/CampaignDetailsComponent';

import './CampaignDetails.scss';

export default function CampaignDetails() {
    const { t, TextTranslationKeys } = useTranslations();

    const { userId } = useUser();

    const { id } = useParams();

    const campaign = useStateSelector(userId, 'campaign', 'campaigns', id);

    return (
        <div className="campaign-details">
            <>
                <ViewNavigationBar showBackLink />

                <div className="header">
                    <span>
                        {t(TextTranslationKeys.campaignView.detail.pageTitle)}
                    </span>
                </div>

                <CampaignDetailsComponent campaign={campaign} />
            </>
        </div>
    );
}
