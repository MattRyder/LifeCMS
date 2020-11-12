import React from 'react';
import { useTranslations } from 'hooks';
import UpdateNameComponent from 'components/Campaign/UpdateNameComponent/UpdateNameComponent';
import FormPage from 'components/Util/FormPage';

export default function CampaignUpdateName() {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <FormPage
            title={t(TextTranslationKeys.campaignView.updateName.pageTitle)}
        >
            <UpdateNameComponent />
        </FormPage>
    );
}
