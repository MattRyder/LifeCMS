import React from 'react';
import { useSelector } from 'react-redux';
import { useParams } from 'react-router';
import { useUser } from 'hooks';
import { findUserNewsletterTemplate } from 'redux/redux-orm/ORM';
import { TemplatesCreate } from '.';

export default function TemplatesDuplicate() {
    const { id } = useParams();

    const { userId } = useUser();

    const newsletter = useSelector((state) => findUserNewsletterTemplate(id)(state, userId));

    return <TemplatesCreate designSource={newsletter.designSource} />;
}
