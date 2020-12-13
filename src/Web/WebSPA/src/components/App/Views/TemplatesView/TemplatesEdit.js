import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router';
import { cx, css } from 'emotion';
import { useUser } from 'hooks';
import { editNewsletterBody } from 'redux/actions/NewsletterTemplateActions';
import Editor from 'components/Newsletters/Editor/Editor';
import { findUserNewsletterTemplate } from 'redux/redux-orm/ORM';

const style = css`
    display: flex;
    flex: 1;
    height: 100%;
`;

export default function NewsletterTemplateEdit() {
    const dispatch = useDispatch();

    const { id } = useParams();

    const { accessToken, userId } = useUser();

    const newsletter = useSelector((state) => findUserNewsletterTemplate(id)(state, userId));

    const onSave = (title, query) => {
        const json = query.serialize();

        dispatch(editNewsletterBody(
            accessToken,
            userId,
            id,
            json,
            '/templates',
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
