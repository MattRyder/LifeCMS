import React from 'react';
import PropTypes from 'prop-types';
import { useSelector } from 'react-redux';
import DatePicker from 'react-datepicker';
import Toggle from 'react-toggle';
import { useFormik } from 'formik';
import {
    useTranslations, useUser, useContentApi,
} from 'hooks';
import getInputFor, { getSelectFor } from 'components/Util/Form';
import { Button, Label, FormGroup } from 'reactstrap';
import { fetchNewsletters } from 'redux/actions/NewsletterTemplateActions';
import { fetchUserProfiles } from 'redux/actions/UserProfileActions';
import Schema, { InitialValues } from './CampaignSchema';

import 'react-datepicker/dist/react-datepicker.css';

import './CampaignFormComponent.scss';

function CampaignFormComponent({ campaign, onFormSubmit }) {
    const { t, TextTranslationKeys } = useTranslations();

    const { accessToken, userId } = useUser();

    const selectedState = useSelector((state) => ({
        newsletter: state.newsletter[userId],
        userProfile: state.userProfile[userId],
    }));

    useContentApi(() => fetchNewsletters(accessToken, userId), accessToken, userId);

    useContentApi(() => fetchUserProfiles(accessToken, userId), accessToken, userId);

    const formik = useFormik({
        initialValues: campaign,
        validationSchema: Schema,
        validateOnBlur: false,
        validateOnChange: false,
        onSubmit: ({
            newsletterTemplateId,
            userProfileId,
            name,
            subjectLineText,
            previewText,
            scheduledDate,
            useSubscriberTimeZone,
        }) => {
            const params = {
                newsletter_template_id: newsletterTemplateId,
                user_profile_id: userProfileId,
                name,
                subject: {
                    subject_line_text: subjectLineText,
                    preview_text: previewText,
                },
                scheduled_date: scheduledDate.toISOString(),
                use_subscriber_time_zone: useSubscriberTimeZone,
            };

            onFormSubmit(params);
        },
    });

    return (
        <form
            className="campaign-form-component"
            onSubmit={formik.handleSubmit}
        >
            {getSelectFor(
                formik,
                'newsletterTemplateId',
                t(TextTranslationKeys.campaign.properties.newsletterTemplate),
                selectedState.newsletter && selectedState.newsletter.newsletters,
            )}

            {getSelectFor(
                formik,
                'userProfileId',
                t(TextTranslationKeys.campaign.properties.userProfile),
                selectedState.userProfile && selectedState.userProfile.userProfiles,
            )}

            { getInputFor(
                formik,
                'name',
                t(TextTranslationKeys.campaign.properties.name),
            )}

            { getInputFor(
                formik,
                'subjectLineText',
                t(TextTranslationKeys.campaign.properties.subjectLineText),
            )}

            { getInputFor(
                formik,
                'previewText',
                t(TextTranslationKeys.campaign.properties.previewText),
            )}

            <FormGroup>
                <Label for="scheduledDate">
                    {
                        t(TextTranslationKeys.campaign.properties.scheduledDate)
                    }
                </Label>
                <DatePicker
                    id="scheduledDate"
                    selected={formik.values.scheduledDate}
                    onChange={(date) => formik.setFieldValue('scheduledDate', date)}
                    showTimeSelect
                    placeholderText={
                        t(TextTranslationKeys.campaignView.create.form.scheduledDatePlaceholder)
                    }
                    timeIntervals={15}
                    dateFormat="MMMM d, yyyy h:mm aa"
                />
            </FormGroup>

            <FormGroup>
                <div className="toggle-container">
                    <Label for="useSubscriberTimeZone">
                        <span>
                            {t(TextTranslationKeys.campaign.properties.useSubscriberTimeZone)}
                        </span>
                    </Label>
                    <div className="react-toggle-container">
                        <Toggle
                            id="useSubscriberTimeZone"
                            defaultChecked={formik.values.useSubscriberTimeZone}
                            onChange={(e) => formik.setFieldValue(
                                'useSubscriberTimeZone',
                                e.currentTarget.value,
                            )}
                        />
                    </div>
                </div>
                <span className="hint">
                    {t(TextTranslationKeys.campaignView.create.form.subscriberHint)}
                </span>
            </FormGroup>

            <Button variant="default" type="submit" disabled={formik.isSubmitting}>
                {t(TextTranslationKeys.common.save)}
            </Button>
        </form>
    );
}

CampaignFormComponent.propTypes = {
    campaign: PropTypes.shape({
        newsletterTemplateId: PropTypes.string,
        userProfileId: PropTypes.string,
        name: PropTypes.string,
        subjectLineText: PropTypes.string,
        previewText: PropTypes.string,
    }),
    onFormSubmit: PropTypes.func.isRequired,
};

CampaignFormComponent.defaultProps = {
    campaign: InitialValues,
};

export default CampaignFormComponent;
