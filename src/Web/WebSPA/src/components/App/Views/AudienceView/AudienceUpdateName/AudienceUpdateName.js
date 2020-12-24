import React from 'react';
import { useTranslations, useUser } from 'hooks';
import UpdateNameComponent from 'components/Audiences/UpdateNameComponent/UpdateNameComponent';
import FormPage from 'components/Util/FormPage';
import { useParams } from 'react-router';
import { useDispatch, useSelector } from 'react-redux';
import { findUserAudience } from 'redux/redux-orm/ORM';
import { updateAudienceName } from 'redux/actions/AudienceActions';

export default function AudienceUpdateName() {
    const { t, TextTranslationKeys } = useTranslations();

    const { accessToken, userId } = useUser();

    const { id } = useParams();

    const dispatch = useDispatch();

    const audience = useSelector((state) => findUserAudience(id)(state, userId));

    const performNameChange = (name) => dispatch(
        updateAudienceName({
            accessToken,
            audienceId: id,
            name,
            redirectTo: `/audiences/${id}`,
        }),
    );

    return (
        <FormPage title={t(TextTranslationKeys.audienceView.updateName.pageTitle)}>
            <UpdateNameComponent
                name={audience && audience.name}
                handleSubmit={(values) => performNameChange(values.name)}
            />
        </FormPage>
    );
}
