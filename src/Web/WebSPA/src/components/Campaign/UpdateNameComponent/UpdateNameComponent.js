import React from 'react';
import { Button } from 'reactstrap';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router';
import { useFormik } from 'formik';
import { updateCampaignName } from 'redux/actions/CampaignActions';
import {
    useTranslations, useUser,
} from 'hooks';
import getInputFor from 'components/Util/Form';
import { findUserCampaign } from 'redux/redux-orm/ORM';
import Schema from './UpdateNameSchema';

export default function UpdateNameComponent() {
    const { t, TextTranslationKeys } = useTranslations();

    const { accessToken, userId } = useUser();

    const { id } = useParams();

    const dispatch = useDispatch();

    const campaign = useSelector((state) => findUserCampaign(id)(state, userId));

    const formik = useFormik({
        initialValues: {
            name: campaign.name,
        },
        validationSchema: Schema,
        validateOnBlur: false,
        validateOnChange: false,
        onSubmit: ({
            name,
        }) => {
            const params = {
                name,
            };

            dispatch(
                updateCampaignName(accessToken, id, params, '/campaigns'),
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
                'name',
                t(TextTranslationKeys.campaign.properties.name),
            )}

            <Button variant="default" type="submit" disabled={formik.isSubmitting}>
                {t(TextTranslationKeys.common.save)}
            </Button>
        </form>
    );
}
