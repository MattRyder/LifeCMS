import React, { useState } from 'react';
import { cx, css } from 'emotion';
import { Button } from 'reactstrap';
import { useEditor } from '@craftjs/core';
import { useTranslations } from 'hooks';
import EditableText from 'components/Util/EditableText';
import { useHistory } from 'react-router';

const styles = {
    topbar: css`
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding: 0.5rem 1rem;
    `,
    nameButton: css`
        border: none;
        border-bottom: 2px dotted #117cfa;
        color: #117cfa;
        margin-left: 0.5rem;
        padding: 0;
    `,
    titleEdit: css`
        display: flex;
        justify-self: center;
        flex: 1;
        height: 100%;
        padding-left: 1rem;
        padding-right: 1rem;

        input {
            margin-right: 0.25rem;
        }
    `,
    editableText: css`
        display: flex;
        
    `,
};

export default function Topbar({ onSave, title }) {
    const history = useHistory();

    const { query } = useEditor();

    const [editorTitle, setEditorTitle] = useState(title);

    const { t, TextTranslationKeys } = useTranslations();

    return (
        <div className={cx(styles.topbar)}>
            <Button
                onClick={() => history.goBack()}
                color="secondary"
            >
                {t(TextTranslationKeys.common.back)}
            </Button>

            <div className={cx(styles.titleEdit)}>
                <EditableText
                    label={t(TextTranslationKeys.newsletterView.editor.title)}
                    text={editorTitle}
                    handleTextChange={(newTitle) => setEditorTitle(newTitle)}
                />
            </div>

            <Button
                type="submit"
                color="primary"
                onClick={() => onSave(editorTitle, query)}
            >
                {t(TextTranslationKeys.common.save)}
            </Button>
        </div>
    );
}
