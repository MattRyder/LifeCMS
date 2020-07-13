import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useRouteMatch } from 'react-router';
import useUser from '../../../../../hooks/useUser';
import Editor from '../../../../Newsletters/Editor/Editor';
import { createNewsletter } from '../../../../../redux/actions/NewsletterActions';

import './NewsletterEdit.scss';
import '../Editor.scss';

export default function NewsletterEdit() {
    const dispatch = useDispatch();

    const { params: { id } } = useRouteMatch();

    const { accessToken, userId } = useUser();

    const newsletter = useSelector((state) => state.newsletter[userId]
            && state.newsletter[userId].newsletters
            && state.newsletter[userId].newsletters.find((n) => n.id === id));

    const onSave = (query) => {
        const json = query.serialize();

        dispatch(createNewsletter(accessToken, userId, {
            name: 'Newsletter Name',
            body: json,
        }));
    };

    return (
        <div className="newsletter-create">
            <Editor
                designSource={newsletter.designSource}
                onSave={onSave}
                title={`Editing a Newsletter: ${newsletter.name}`}
            />
        </div>
    );
}
