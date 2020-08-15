import React from 'react';
import { useDispatch } from 'react-redux';
import { useParams } from 'react-router';
import { useUser, useTranslations, useStateSelector } from '../../../../../hooks';
import { editNewsletterBody } from '../../../../../redux/actions/NewsletterActions';
import Editor from '../../../../Newsletters/Editor/Editor';

import '../Editor.scss';
import './NewsletterEdit.scss';

export default function NewsletterEdit() {
    const dispatch = useDispatch();

    const { id } = useParams();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const newsletter = useStateSelector(userId, 'newsletter', 'newsletters', id);

    const onSave = (query) => {
        const json = query.serialize();

        dispatch(editNewsletterBody(accessToken, userId, id, json, '/newsletters'));
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
