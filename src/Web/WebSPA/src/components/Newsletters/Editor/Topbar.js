import React, { useState } from 'react';
import { cx, css } from 'emotion';
import { Button } from 'reactstrap';
import { useEditor } from '@craftjs/core';

import Icon, { Icons } from 'components/App/Iconography/Icon';
import { useTranslations } from 'hooks';

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

        input {
            margin-right: 0.25rem;
        }
    `,
};

export default function Topbar({ onSave, title }) {
    const { query } = useEditor();

    const [isEditable, setEditable] = useState(false);

    const toggleEditable = () => setEditable(!isEditable);

    const [editorTitle, setEditorTitle] = useState(title);

    const { t, TextTranslationKeys } = useTranslations();

    return (
        <div className={cx(styles.topbar)}>
            {isEditable ? (
                <div className={cx(styles.titleEdit)}>
                    <input
                        type="text"
                        value={editorTitle}
                        onChange={(e) => setEditorTitle(e.currentTarget.value)}
                    />
                    <Button
                        type="button"
                        color="primary"
                        size="sm"
                        onClick={toggleEditable}
                    >
                        <Icon icon={Icons.check} />
                    </Button>
                </div>
            ) : (
                <div>
                    <span>
                        {t(TextTranslationKeys.newsletterView.editor.title)}
                    </span>
                    <button
                        type="button"
                        className={cx(styles.nameButton)}
                        onClick={toggleEditable}
                    >
                        {editorTitle}
                    </button>
                </div>
            )}

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
