import React from 'react';
import { connect } from 'react-redux';
import { useFormik } from 'formik';
import { useTranslation } from 'react-i18next';
import {
    Row, Col, Input, Button, FormFeedback,
} from 'reactstrap';
import TextTranslationKeys from '../../../i18n/TextTranslationKeys';
import Schema, { InitialValues } from './CreatePostSchema';
import { createPost } from '../../../redux/actions/PostActions';

import './CreatePostComponent.scss';

const mapDispatchToProps = (dispatch) => ({
    dispatchCreatePost: (accessToken, postParams) => dispatch(
        createPost(accessToken, postParams),
    ),
});

function CreatePostComponent({ accessToken, dispatchCreatePost }) {
    const formik = useFormik({
        initialValues: InitialValues,
        validationSchema: Schema,
        onSubmit: (values) => {
            const params = {
                Title: values.title,
                Text: values.text,
            };

            dispatchCreatePost(accessToken, params);
        },
    });

    const { t } = useTranslation();

    return (
        <div className="create-post-component">
            <form onSubmit={formik.handleSubmit}>
                <Row>
                    <Col md="10">
                        <Input
                            placeholder={t(TextTranslationKeys.post.create.title.placeholder)}
                            name="title"
                            onChange={formik.handleChange}
                            invalid={formik.errors.title}
                            value={formik.values.title}
                        />
                        <FormFeedback>{formik.errors.title}</FormFeedback>
                    </Col>
                    <Col md="2">
                        <Button variant="primary" type="submit" block disabled={formik.isSubmitting}>
                            {t(TextTranslationKeys.common.post)}
                        </Button>
                    </Col>
                </Row>
                <Row>
                    <Col md="12">
                        <Input
                            placeholder={t(TextTranslationKeys.post.create.text.placeholder)}
                            name="text"
                            type="textarea"
                            onChange={formik.handleChange}
                            invalid={formik.errors.text}
                            value={formik.values.text}
                        />
                        <FormFeedback>{formik.errors.text}</FormFeedback>
                    </Col>
                </Row>
            </form>
        </div >
    )
}

export default connect(null, mapDispatchToProps)(CreatePostComponent);
