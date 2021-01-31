import React from 'react';
import PropTypes from 'prop-types';
import { useSelector } from 'react-redux';
import DatePicker from 'react-datepicker';
import Toggle from 'react-toggle';
import { css, cx } from 'emotion';
import { useFormik } from 'formik';
import {
    useTranslations, useUser, useContentApi,
} from 'hooks';
import getInputFor, { FormikSelect } from 'components/Util/Form';
import { Button, Label, FormGroup } from 'reactstrap';
import { fetchNewsletters } from 'redux/actions/NewsletterTemplateActions';
import { fetchUserProfiles } from 'redux/actions/UserProfileActions';
import {
    findUserNewsletterTemplates, findUserUserProfiles,
} from 'redux/redux-orm/ORM';
import { boxShadow } from 'theme';
import Schema, { InitialValues } from './CampaignSchema';

import 'react-datepicker/dist/react-datepicker.css';

const styles = {
    hint: css`
        color: #999;
        font-size: smaller;
    `,
    form: css`
        background-color: #fff;
        display: flex;
        flex-direction: column;
        justify-content: space-around;
        padding: 1rem;
        ${boxShadow('rgba(0, 0, 0, 0.05)')}

        > div {
            padding: 0.5rem 0;
        }
    `,
    toggleContainer: {
        parent: css`
            display: flex;

            label {
                margin-bottom: 0;
            }
        `,
        reactToggle: css`
            display: flex;
            align-items: center;
            margin-left: 1rem;
        `,
    },
    datePicker: css`
        --datepicker-width: 175px;
        .react-datepicker-wrapper,
        input#scheduledDate {
            width: 100%;
        }

        .react-datepicker-wrapper {
            width: 100%;
        }

        .react-datepicker__time-container {
            .react-datepicker__time .react-datepicker__time-box {
                width: var(--datepicker-width);
            }

            width: var(--datepicker-width);
        }

        .react-datepicker__navigation--next--with-time:not(.react-datepicker__navigation--next--with-today-button) {
            right: calc(var(--datepicker-width) + 15px);
        }
    `,
};

function CampaignFormComponent({ campaign, onFormSubmit }) {
    const { t, TextTranslationKeys } = useTranslations();

    const { accessToken, userId } = useUser();

    const selectedState = useSelector((state) => ({
        newsletterTemplates: findUserNewsletterTemplates(state, userId),
        userProfiles: findUserUserProfiles(state, userId),
    }));

    useContentApi(() => fetchNewsletters(accessToken, userId), accessToken);

    useContentApi(() => fetchUserProfiles(accessToken), accessToken);

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
            className={cx(styles.form)}
            onSubmit={formik.handleSubmit}
        >
            <FormikSelect
                formik={formik}
                inputName="newsletterTemplateId"
                label={t(TextTranslationKeys.campaign.properties.newsletterTemplate)}
                collection={selectedState.newsletterTemplates && selectedState.newsletterTemplates}
            />

            <FormikSelect
                formik={formik}
                inputName="userProfileId"
                label={t(TextTranslationKeys.campaign.properties.userProfile)}
                collection={selectedState.userProfiles && selectedState.userProfiles}
            />

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
                    {t(TextTranslationKeys.campaign.properties.scheduledDate)}
                </Label>
                <div className={styles.datePicker}>
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
                </div>
            </FormGroup>

            <FormGroup>
                <div className={cx(styles.toggleContainer.parent)}>
                    <Label for="useSubscriberTimeZone">
                        <span>
                            {t(TextTranslationKeys.campaign.properties.useSubscriberTimeZone)}
                        </span>
                    </Label>
                    <div className={cx(styles.toggleContainer.reactToggle)}>
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
                <span className={cx(styles.hint)}>
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
