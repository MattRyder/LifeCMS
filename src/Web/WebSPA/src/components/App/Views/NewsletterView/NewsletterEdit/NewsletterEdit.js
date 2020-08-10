import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useRouteMatch } from 'react-router';
import { editNewsletter } from '../../../../../redux/actions/NewsletterActions';
import { useUser, useTranslations } from '../../../../../hooks';
import Editor from '../../../../Newsletters/Editor/Editor';

import '../Editor.scss';
import './NewsletterEdit.scss';

export default function NewsletterEdit() {
    const dispatch = useDispatch();

    const { params: { id } } = useRouteMatch();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const newsletter = useSelector((state) => state.newsletter[userId]
            && state.newsletter[userId].newsletters
            && state.newsletter[userId].newsletters.find((n) => n.id === id));

    const onSave = (query) => {
        const json = query.serialize();

        dispatch(editNewsletter(accessToken, userId, id, json));
    };

    return (
        <div className="newsletter-edit">
            <Editor
                designSource={newsletter.designSource}
                onSave={onSave}
                title={`${t(TextTranslationKeys.newsletterView.editorTitleEdit)}: ${newsletter.name}`}
            />
        </div>
    );
}
