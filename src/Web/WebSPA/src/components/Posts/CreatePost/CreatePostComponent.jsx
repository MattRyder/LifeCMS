import React from 'react';
import { useFormik } from 'formik';
import { Input, Button, FormFeedback } from 'reactstrap';
import Icon, { Icons } from '../../App/Iconography/Icon';
import Schema, { InitialValues } from './CreatePostSchema';
import { createPost } from '../../../redux/actions/PostActions';

import './CreatePostComponent.scss';
import { useTranslations } from '../../../hooks';

export default function ({ accessToken }) {
    const formik = useFormik({
        initialValues: InitialValues,
        validationSchema: Schema,
        onSubmit: (values) => {
            const params = {
                Title: 'Dummy Title',
                Text: values.text,
            };

            createPost(accessToken, params).then(() => formik.resetForm());
        },
    });

    const handleTextareaKeyDown = (e) => {
        const el = e.currentTarget;

        setTimeout(() => {
            el.style.cssText = 'height:auto; padding:0';
            el.style.cssText = `height: ${el.scrollHeight}px`;
        }, 0);
    };

    const { t, TextTranslationKeys } = useTranslations();

    return (
        <div className="create-post-component">
            <form onSubmit={formik.handleSubmit}>
                <Input
                    placeholder={t(TextTranslationKeys.post.create.text.placeholder)}
                    name="text"
                    type="textarea"
                    onKeyDown={handleTextareaKeyDown}
                    onChange={formik.handleChange}
                    invalid={formik.errors.text}
                    value={formik.values.text}
                />
                <FormFeedback />
                <Button variant="default" type="submit" disabled={formik.isSubmitting}>
                    {t(TextTranslationKeys.common.post)}
                </Button>
            </form>
            <ul className="add-media-list">
                <li>
                    <Icon icon={Icons.photo} />
                    <span>Add Picture or Video</span>
                </li>
                <li>
                    <Icon icon={Icons.logo} />
                    <span>Take thoughts</span>
                </li>
                <li>
                    <Icon icon={Icons.ellipsisHorizontal} />
                </li>
            </ul>
        </div>
    );
}
