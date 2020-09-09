import React from 'react';
import { useTranslations } from 'hooks';
import UpdateSubjectComponent from 'components/Campaign/UpdateSubjectComponent/UpdateSubjectComponent';
import ViewNavigationBar from 'components/App/Components/ViewNavigationBar/ViewNavigationBar';

export default function CampaignUpdateSubject() {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <>
            <ViewNavigationBar showBackLink />

            <div className="campaign-form-container">
                <>
                    <div className="header">
                        <span>
                            {t(TextTranslationKeys.campaignView.updateSubject.pageTitle)}
                        </span>
                    </div>

                    <UpdateSubjectComponent />
                </>
            </div>
        </>
    );
}
