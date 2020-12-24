import React from 'react';
import AudienceFormComponent from 'components/Audiences/AudienceFormComponent/AudienceFormComponent';
import FormPage from 'components/Util/FormPage';
import { useTranslations, useUser } from 'hooks';
import { createAudienceAndRedirect } from 'redux/actions/AudienceActions';
import { useDispatch } from 'react-redux';

export default function AudienceCreate() {
    const { accessToken } = useUser();

    const dispatch = useDispatch();

    const { t, TextTranslationKeys } = useTranslations();

    const handleFormSubmit = ({ name }) => {
        dispatch(createAudienceAndRedirect(accessToken, { name }));
    };

    return (
        <FormPage
            title={t(TextTranslationKeys.audienceView.create.pageTitle)}
        >
            <AudienceFormComponent onFormSubmit={handleFormSubmit} />
        </FormPage>
    );
}

AudienceCreate.propTypes = {};

AudienceCreate.defaultProps = {};
