import React from 'react';
import { useTranslations } from 'hooks';
import UpdateNameComponent from 'components/Campaign/UpdateNameComponent/UpdateNameComponent';
import ViewNavigationBar from 'components/App/Components/ViewNavigationBar/ViewNavigationBar';

export default function CampaignUpdateName() {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <>
            <ViewNavigationBar showBackLink />

            <div className="campaign-form-container">
                <>
                    <div className="header">
                        <span>
                            {t(TextTranslationKeys.campaignView.updateName.pageTitle)}
                        </span>
                    </div>

                    <UpdateNameComponent />
                </>
            </div>
        </>
    );
}
