import React from 'react';
import { useDispatch } from 'react-redux';
import { useTranslations, useUser } from 'hooks';
import { createCampaign } from 'redux/actions/CampaignActions';
import CampaignFormComponent from 'components/Campaign/CampaignFormComponent/CampaignFormComponent';
import FormPage from 'components/Util/FormPage';

export default function CampaignCreate() {
    const { t, TextTranslationKeys } = useTranslations();

    const dispatch = useDispatch();

    const { accessToken, userId } = useUser();

    const handleSubmit = (params) => dispatch(
        createCampaign(accessToken, userId, params, '/campaigns'),
    );

    return (
        <FormPage title={t(TextTranslationKeys.campaignView.create.pageTitle)}>
            <CampaignFormComponent onFormSubmit={handleSubmit} />
        </FormPage>
    );
}
