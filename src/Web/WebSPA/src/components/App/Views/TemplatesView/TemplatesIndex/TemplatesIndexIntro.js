import React from 'react';

import {
    ReactComponent as TemplatesIndexIntroIllustration,
} from 'assets/illustrations/two-people-illustration.svg';
import { useTranslations } from 'hooks';
import IntroView from '../../IntroView';

export default function TemplatesIndexIntro() {
    const { t, TextTranslationKeys } = useTranslations();

    const data = {
        ctaText: t(TextTranslationKeys.newsletterView.index.createTemplateCta),
        ctaTo: '/templates/new',
        resourceDescription: t(TextTranslationKeys.template.description),
        resourceTitle: t(TextTranslationKeys.template.displayName),
    };

    return (
        <IntroView
            ctaTo={data.ctaTo}
            ctaText={data.ctaText}
            resourceDescription={data.resourceDescription}
            resourceTitle={data.resourceTitle}
        >
            <TemplatesIndexIntroIllustration />
        </IntroView>
    );
}
