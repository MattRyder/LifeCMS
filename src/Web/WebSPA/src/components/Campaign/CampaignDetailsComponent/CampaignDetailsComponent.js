import React from 'react';
import { useTranslations } from 'hooks';
import DetailRow from 'components/Util/DetailPage/DetailRow';
import Meta from 'components/Util/DetailPage/Meta';
import Paper from 'components/Util/Paper';
import { formatTimestampDate } from 'components/Util/Date';

export default function CampaignDetailsComponent({
    campaign: {
        id,
        name,
        scheduledDate,
        subjectLineText,
        previewText,
        createdAt,
        updatedAt,
    },
}) {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <>
            <Paper>
                <DetailRow
                    label={t(TextTranslationKeys.campaign.properties.name)}
                    value={<span>{name}</span>}
                    linkTo={`/campaigns/${id}/update-name`}
                    linkText={t(TextTranslationKeys.common.update)}
                />

                <DetailRow
                    label={t(TextTranslationKeys.campaignView.detail.subjectDetailTitle)}
                    value={(
                        <>
                            <span>{subjectLineText}</span>
                            <span>{previewText}</span>
                        </>
                    )}
                    linkTo={`/campaigns/${id}/update-subject`}
                    linkText={t(TextTranslationKeys.common.update)}
                />

                <DetailRow
                    label={t(TextTranslationKeys.campaign.properties.scheduledDate)}
                    value={formatTimestampDate(scheduledDate)}
                    // linkTo: `/campaigns/${id}/update-scheduled-date`,
                    linkText={t(TextTranslationKeys.campaignView.detail.updateName)}
                />
            </Paper>
            <Meta keyValues={[
                {
                    label: t(TextTranslationKeys.common.createdAt),
                    value: formatTimestampDate(createdAt),
                },
                {
                    label: t(TextTranslationKeys.common.updatedAt),
                    value: formatTimestampDate(updatedAt),
                },
            ]}
            />
        </>
    );
}
