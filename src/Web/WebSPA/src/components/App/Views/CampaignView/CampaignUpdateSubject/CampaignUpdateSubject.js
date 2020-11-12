import React from 'react';
import { useTranslations } from 'hooks';
import UpdateSubjectComponent from 'components/Campaign/UpdateSubjectComponent/UpdateSubjectComponent';
import FormPage from 'components/Util/FormPage';

export default function CampaignUpdateSubject() {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <FormPage
            title={t(TextTranslationKeys.campaignView.updateSubject.pageTitle)}
        >
            <UpdateSubjectComponent />
        </FormPage>
    );
}
