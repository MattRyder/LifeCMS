import React from 'react';
import { useSelector } from 'react-redux';
import { Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import { useContentApi, useUser, useTranslations } from '../../../../../hooks';
import { fetchCampaigns } from '../../../../../redux/actions/CampaignActions';
import TableComponent from '../../../../Util/Table/TableComponent';
import CampaignRowComponent from './CampaignRowComponent';

import './CampaignIndex.scss';

export default function CampaignIndex() {
    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const campaignState = useSelector(
        (state) => state.campaign[userId],
    );

    useContentApi(() => fetchCampaigns(accessToken, userId), accessToken, userId);

    return (
        <div className="campaign-index padded">
            <div className="title">
                <h2>{t(TextTranslationKeys.campaignView.index.pageTitle)}</h2>
                <Button tag={Link} size="sm" color="primary" to="/campaigns/new">
                    {t(TextTranslationKeys.campaignView.index.createCampaign)}
                </Button>
            </div>
            <TableComponent
                headings={[
                    t(TextTranslationKeys.campaign.properties.name),
                    t(TextTranslationKeys.campaignView.index.scheduledFor),
                    t(TextTranslationKeys.campaignView.index.createdAt),
                    '',
                ]}
                rowComponent={CampaignRowComponent}
                collection={campaignState && campaignState.campaigns}
            />
        </div>
    );
}
