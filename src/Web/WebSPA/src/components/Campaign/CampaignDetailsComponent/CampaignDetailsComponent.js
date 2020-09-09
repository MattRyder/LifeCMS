import React from 'react';
import { format, parseISO } from 'date-fns';
import { useTranslations } from 'hooks';
import { Button } from 'reactstrap';
import { Link } from 'react-router-dom';

import './CampaignDetailsComponent.scss';

const DATE_FORMAT = 'E, MMMM dd hh:mm a';

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

    const makeDetail = ({
        name, value, linkTo, linkText,
    }) => (
        <div className="detail">
            <h4>{name}</h4>
            <div>
                {value}
                {linkTo && (
                    <Button color="link" tag={Link} to={linkTo}>
                        {linkText}
                    </Button>
                )}
            </div>
        </div>
    );

    return (
        <>
            <div className="campaign-details-component">
                {
                    makeDetail({
                        name: t(TextTranslationKeys.campaign.properties.name),
                        value: <span>{name}</span>,
                        linkTo: `/campaigns/${id}/update-name`,
                        linkText: t(TextTranslationKeys.common.update),
                    })
                }

                <div className="subject">
                    {
                        makeDetail({
                            name: t(TextTranslationKeys.campaignView.detail.subjectDetailTitle),
                            value: (
                                <>
                                    <span>{subjectLineText}</span>
                                    <span>{previewText}</span>
                                </>
                            ),
                            linkTo: `/campaigns/${id}/update-subject`,
                            linkText: t(TextTranslationKeys.common.update),
                        })
                    }
                </div>

                {
                    makeDetail({
                        name: t(TextTranslationKeys.campaign.properties.scheduledDate),
                        value: format(parseISO(scheduledDate), DATE_FORMAT),
                        // linkTo: `/campaigns/${id}/update-scheduled-date`,
                        linkText: t(TextTranslationKeys.campaignView.detail.updateName),
                    })
                }
            </div>
            <div className="meta">
                <p>
                    {t(TextTranslationKeys.common.createdAt)}
                    {': '}
                    {format(parseISO(createdAt), DATE_FORMAT)}
                </p>
                <p>
                    {t(TextTranslationKeys.common.updatedAt)}
                    {': '}
                    {format(parseISO(updatedAt), DATE_FORMAT)}
                </p>
            </div>
        </>
    );
}
