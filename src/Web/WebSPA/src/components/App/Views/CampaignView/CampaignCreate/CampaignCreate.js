import React from 'react';
import { useDispatch } from 'react-redux';
import { useTranslations, useUser } from 'hooks';
import { createCampaign } from 'redux/actions/CampaignActions';
import CampaignFormComponent from 'components/Campaign/CampaignFormComponent/CampaignFormComponent';
import ViewNavigationBar from 'components/App/Components/ViewNavigationBar/ViewNavigationBar';

export default function CampaignCreate() {
    const { t, TextTranslationKeys } = useTranslations();

    const dispatch = useDispatch();

    const { accessToken, userId } = useUser();

    const handleSubmit = (params) => dispatch(
        createCampaign(accessToken, userId, params, '/campaigns'),
    );

    return (
        <div className="campaign-create">
            <>
                <ViewNavigationBar showBackLink />

                <div className="campaign-form-container">
                    <div className="header">
                        <span>
                            {t(TextTranslationKeys.campaignView.create.pageTitle)}
                        </span>
                    </div>

                    <CampaignFormComponent onFormSubmit={handleSubmit} />
                </div>
            </>
        </div>
    );
}
