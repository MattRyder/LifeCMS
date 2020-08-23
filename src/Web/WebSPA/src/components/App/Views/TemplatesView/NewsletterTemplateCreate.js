import React from 'react';
import { useDispatch } from 'react-redux';
import { createNewsletterTemplate } from '../../../../redux/actions/NewsletterTemplateActions';
import { useUser, useTranslations } from '../../../../hooks';
import Editor from '../../../Newsletters/Editor/Editor';

import './Editor.scss';
import './NewsletterTemplateCreate.scss';

export default function NewsletterTemplateCreate() {
    const dispatch = useDispatch();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const onSave = (query) => {
        const json = query.serialize();

        dispatch(createNewsletterTemplate(accessToken, userId, {
            name: 'Newsletter Template Name',
            body: json,
        },
        '/newsletter/templates'));
    };

    return (
        <div className="newsletter-template-create">
            <Editor
                title={t(TextTranslationKeys.newsletterView.editorTitleCreate)}
                onSave={onSave}
            />
        </div>
    );
}
