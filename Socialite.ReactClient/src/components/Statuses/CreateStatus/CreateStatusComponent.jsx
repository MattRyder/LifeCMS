import React from 'react';
import { connect } from 'react-redux';
import {
    InputGroup, InputGroupAddon, Input, Button, Row, Col,
} from 'reactstrap';
import { useTranslation } from 'react-i18next';
import { useFormik } from 'formik';
import { createStatus } from '../../../redux/actions/StatusActions';
import TextTranslationKeys from '../../../i18n/TextTranslationKeys';
import Schema, { InitialValues } from './CreateStatusSchema';
import EmojiDropdownComponent from './EmojiDropdownComponent';

import './CreateStatusComponent.scss';

const mapDispatchToProps = (dispatch) => ({
    dispatchCreateStatus: (accessToken, statusParams) => dispatch(
        createStatus(accessToken, statusParams),
    ),
});

const CreateStatusComponent = ({ accessToken, dispatchCreateStatus }) => {
    const formik = useFormik({
        initialValues: InitialValues,
        // validationSchema: Schema,
        onSubmit: (values) => {
            const params = {
                Mood: values.mood,
                Text: values.text,
            };

            dispatchCreateStatus(accessToken, params);
        },
    });

    const onEmojiClick = (emoji) => formik.setFieldValue('mood', emoji);

    const { t } = useTranslation();

    return (
        <div className="create-status-component">
            <form onSubmit={formik.handleSubmit}>
                <InputGroup>
                    <InputGroupAddon addonType="prepend">
                        <EmojiDropdownComponent onEmojiClick={onEmojiClick} />
                    </InputGroupAddon>
                    <Input
                        placeholder={t(TextTranslationKeys.status.create.placeholder)}
                        name="text"
                        onChange={formik.handleChange}
                        invalid={formik.errors.text}
                        value={formik.values.text}
                    />
                </InputGroup>
                <Row>
                    <Col md={{ size: 2, offset: 10 }}>
                        <Button variant="primary" type="submit" block disabled={formik.isSubmitting}>
                            {t(TextTranslationKeys.common.post)}
                        </Button>
                    </Col>
                </Row>
            </form>
        </div>
    );
};

export default connect(null, mapDispatchToProps)(CreateStatusComponent);
