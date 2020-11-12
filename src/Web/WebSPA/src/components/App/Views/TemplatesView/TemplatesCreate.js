import React from 'react';
import { cx, css } from 'emotion';
import { useDispatch } from 'react-redux';
import { createNewsletterTemplate } from 'redux/actions/NewsletterTemplateActions';
import Editor from 'components/Newsletters/Editor/Editor';
import { useUser, useTranslations } from 'hooks';

const style = css`
    display: flex;
    flex: 1;
    height: 100%;
`;

export default function NewsletterTemplateCreate() {
    const dispatch = useDispatch();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const onSave = (title, query) => {
        const params = {
            name: title,
            body: query.serialize(),
        };

        dispatch(
            createNewsletterTemplate(
                accessToken,
                userId,
                params,
                '/templates',
            ),
        );
    };

    return (
        <div className={cx(style)}>
            <Editor
                title={t(TextTranslationKeys.newsletterView.editorTitleCreate)}
                onSave={onSave}
            />
        </div>
    );
}
