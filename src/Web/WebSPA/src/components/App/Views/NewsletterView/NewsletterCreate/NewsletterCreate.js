import React from 'react';
import { useDispatch } from 'react-redux';
import { createNewsletter } from '../../../../../redux/actions/NewsletterActions';
import { useUser, useTranslations } from '../../../../../hooks';
import Editor from '../../../../Newsletters/Editor/Editor';

import '../Editor.scss';
import './NewsletterCreate.scss';

export default function NewsletterCreate() {
    const dispatch = useDispatch();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const onSave = (query) => {
        const json = query.serialize();

        dispatch(createNewsletter(accessToken, userId, {
            name: 'Newsletter Name',
            body: json,
        },
        '/newsletters'));
    };

    return (
        <div className="newsletter-create">
            <Editor
                title={t(TextTranslationKeys.newsletterView.editorTitleCreate)}
                onSave={onSave}
            />
        </div>
    );
}
