import React from 'react';

import { ReactComponent as CampaignIllustration } from 'assets/illustrations/campaign-illustration.svg';
import { useTranslations } from 'hooks';
import IntroView from '../../IntroView';

export default function CampaignIndexIntro() {
    const { t, TextTranslationKeys } = useTranslations();

    const data = {
        ctaText: t(TextTranslationKeys.campaignView.index.createCampaignCta),
        ctaTo: '/campaigns/new',
        resourceDescription: t(TextTranslationKeys.campaign.description),
        resourceTitle: t(TextTranslationKeys.campaign.displayName),
    };

    return (
        <IntroView
            ctaTo={data.ctaTo}
            ctaText={data.ctaText}
            resourceDescription={data.resourceDescription}
            resourceTitle={data.resourceTitle}
        >
            <CampaignIllustration />
        </IntroView>
    );
}
