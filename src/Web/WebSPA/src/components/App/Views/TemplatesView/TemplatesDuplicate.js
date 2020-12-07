import React from 'react';
import { useParams } from 'react-router';
import { useUser, useStateSelector } from 'hooks';
import { TemplatesCreate } from '.';

export default function TemplatesDuplicate() {
    const { id } = useParams();

    const { userId } = useUser();

    const newsletter = useStateSelector(
        userId,
        'newsletter',
        'newsletters',
        id,
    );

    return <TemplatesCreate designSource={newsletter.designSource} />;
}
