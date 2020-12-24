import React from 'react';

import { ReactComponent as AudienceIllustration } from 'assets/illustrations/audience-illustration.svg';
import { useTranslations } from 'hooks';
import IntroView from '../../IntroView';

export default function AudienceIntro() {
    const { t, TextTranslationKeys } = useTranslations();

    const data = {
        ctaText: t(TextTranslationKeys.audienceView.intro.ctaText),
        ctaTo: '/audiences/new',
        resourceDescription: t(TextTranslationKeys.audience.description),
        resourceTitle: t(TextTranslationKeys.audience.displayName),
    };

    return (
        <IntroView
            ctaTo={data.ctaTo}
            ctaText={data.ctaText}
            resourceDescription={data.resourceDescription}
            resourceTitle={data.resourceTitle}
        >
            <AudienceIllustration />
        </IntroView>
    );
}
