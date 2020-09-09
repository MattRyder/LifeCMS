import React from 'react';
import { Button } from 'reactstrap';
import { useDispatch } from 'react-redux';
import { useParams } from 'react-router';
import { useFormik } from 'formik';
import { updateCampaignSubject } from 'redux/actions/CampaignActions';
import {
    useTranslations, useUser, useStateSelector,
} from 'hooks';
import getInputFor from 'components/Util/Form';
import Schema from './UpdateSubjectSchema';

export default function UpdateSubjectComponent() {
    const { t, TextTranslationKeys } = useTranslations();

    const { accessToken, userId } = useUser();

    const { id } = useParams();

    const dispatch = useDispatch();

    const campaign = useStateSelector(userId, 'campaign', 'campaigns', id);

    const formik = useFormik({
        initialValues: {
            previewText: campaign.previewText,
            subjectLineText: campaign.subjectLineText,
        },
        validationSchema: Schema,
        validateOnBlur: false,
        validateOnChange: false,
        onSubmit: ({
            previewText,
            subjectLineText,
        }) => {
            const params = {
                preview_text: previewText,
                subject_line_text: subjectLineText,
            };

            dispatch(
                updateCampaignSubject(accessToken, userId, id, params, '/campaigns'),
            );
        },
    });

    return (
        <form
            className="campaign-form-component"
            onSubmit={formik.handleSubmit}
        >
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

            <Button variant="default" type="submit" disabled={formik.isSubmitting}>
                {t(TextTranslationKeys.common.save)}
            </Button>
        </form>
    );
}
