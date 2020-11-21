import React from 'react';
import { cx, css } from 'emotion';
import { v4 as uuidv4 } from 'uuid';
import { useDispatch } from 'react-redux';
import { createNewsletterTemplate } from 'redux/actions/NewsletterTemplateActions';
import Editor from 'components/Newsletters/Editor/Editor';
import { useUser, useTranslations } from 'hooks';
import { useParams } from 'react-router';
import { redirectWithError } from 'redux/actions/AppActions';
import UploadFileService from 'services/UploadFileService';
import DesignSources from './DesignSources';

const style = css`
    display: flex;
    flex: 1;
    height: 100%;
`;

async function ProcessNode(node, accessToken) {
    const localNode = node;

    if (localNode.type && node.type.resolvedName === 'Image') {
        const { contentType, url } = localNode.props.file;

        const fileUrl = await UploadFileService(
            accessToken,
            uuidv4(),
            contentType,
            url,
        );

        localNode.props.file.url = fileUrl;
    }

    return localNode;
}

async function ProcessBody(craftJsBodyString, accessToken) {
    const craftJsNodes = JSON.parse(craftJsBodyString);

    const body = {};

    // eslint-disable-next-line no-restricted-syntax
    for (const [key, node] of Object.entries(craftJsNodes)) {
        body[key] = await ProcessNode(node, accessToken);
    }

    return JSON.stringify(body);
}

export function TemplateCreateFromSlug() {
    const { designSourceSlug } = useParams();

    const dispatch = useDispatch();

    const designSource = DesignSources[designSourceSlug];

    if (!designSource) {
        dispatch(redirectWithError(
            '/templates/new',
            'That template is not available or does not exist.',
        ));
    }

    return (
        <NewsletterTemplateCreate
            designSource={designSource && designSource.markup}
        />
    );
}

export default function NewsletterTemplateCreate({ designSource }) {
    const dispatch = useDispatch();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const onSave = async (title, query) => {
        const params = {
            name: title,
            body: await ProcessBody(query.serialize(), accessToken),
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
                designSource={designSource}
                title={t(TextTranslationKeys.newsletterView.editorTitleCreate)}
                onSave={onSave}
            />
        </div>
    );
}
