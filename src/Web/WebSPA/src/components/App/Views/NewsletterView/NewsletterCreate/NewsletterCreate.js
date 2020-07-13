import React from 'react';
import { useDispatch } from 'react-redux';
import useUser from '../../../../../hooks/useUser';
import Editor from '../../../../Newsletters/Editor/Editor';
import { createNewsletter } from '../../../../../redux/actions/NewsletterActions';

import './NewsletterCreate.scss';
import '../Editor.scss';

export default function NewsletterCreate() {
    const dispatch = useDispatch();

    const { accessToken, userId } = useUser();

    const onSave = (query) => {
        const json = query.serialize();

        dispatch(createNewsletter(accessToken, userId, {
            name: 'Newsletter Name',
            body: json,
        }));
    };

    return (
        <div className="newsletter-create">
            <Editor title="Create a Newsletter" onSave={onSave} />
        </div>
    );
}
