import React from 'react';
import { useDispatch } from 'react-redux';
import { useParams } from 'react-router';
import { cx, css } from 'emotion';
import { useUser, useStateSelector } from 'hooks';
import { editNewsletterBody } from 'redux/actions/NewsletterTemplateActions';
import Editor from 'components/Newsletters/Editor/Editor';

const style = css`
    display: flex;
    flex: 1;
    height: 100%;
`;

export default function NewsletterTemplateEdit() {
    const dispatch = useDispatch();

    const { id } = useParams();

    const { accessToken, userId } = useUser();

    const newsletter = useStateSelector(
        userId,
        'newsletter',
        'newsletters',
        id,
    );

    const onSave = (title, query) => {
        const json = query.serialize();

        dispatch(editNewsletterBody(
            accessToken,
            userId,
            id,
            json,
            '/newsletter/templates',
        ));
    };

    return (
        <div className={cx(style)}>
            <Editor
                designSource={newsletter.designSource}
                onSave={onSave}
                title={`${newsletter.name}`}
            />
        </div>
    );
}
