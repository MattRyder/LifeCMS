import React from 'react';
import { useEditor } from '@craftjs/core';
import { css, cx } from 'emotion';
import { useTranslations } from 'hooks';
import { Alert } from 'reactstrap';
import { Columns, Row, Text } from './Components';
import { RowIcon, ColumnsIcon, FreeTextIcon } from './Toolbox/Icons';

const style = {
    helpAlert: css`
        font-size: 0.85em;
        margin: 0.5em;
    `,
    button: css`
        font-size: 14px;
        padding: 6px 12px;
        margin-bottom: 0;
        display: block;
        text-decoration: none;
        text-align: center;
        white-space: nowrap;
        vertical-align: middle;
        touch-action: manipulation;
        cursor: pointer;
        user-select: none;
        background-image: none;
        border: 1px solid transparent;
    `,
    toolbox: css`
        padding: 0.5rem;
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(100px, 1fr));
        grid-gap: 1rem;
    `,
    toolboxItem: css`
        display: grid;
        flex: 0.5;
        align-items: center;
        justify-content: center;
        background-color: white !important;
        border-radius: 5px;
        border-color: rgba(0, 0, 0, 0.1) !important;
        padding: 0.25rem 0.25rem;
        &::before {
            grid-area: 1 / 1 / 2 / 2;
            content: "";
            display: block;
            padding-bottom: 75%;
        }
        span {
            grid-area: 1 / 1 / 2 / 2;
        }
        &:hover {
            box-shadow: 0 1px 1px rgba(0,0,0,0.05), 
              0 2px 2px rgba(0,0,0,0.05), 
              0 4px 4px rgba(0,0,0,0.05), 
              0 6px 8px rgba(0,0,0,0.05),
              0 8px 16px rgba(0,0,0,0.05);
            background-color: #EFEFEF;
            color: blue !important;
        }
    `,
    buttonText: css`
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    `,
    buttonIcon: css`
        width: 20%;
        height: auto;
    `,
};

const elements = (t, ttk) => [
    {
        key: 'row',
        icon: RowIcon,
        component: <Row />,
        name: t(ttk.newsletterView.editor.toolbox.row),
    },
    {
        key: 'freeText',
        icon: FreeTextIcon,
        component: <Text />,
        name: t(ttk.newsletterView.editor.toolbox.freeText),
    },
    {
        key: 'columns',
        icon: ColumnsIcon,
        component: <Columns />,
        name: t(ttk.newsletterView.editor.toolbox.columns),
    },
];

export default function Toolbox() {
    const { connectors: { create } } = useEditor();

    const { t, TextTranslationKeys } = useTranslations();

    return (
        <>
            <Alert className={cx(style.helpAlert)} color="info">
                {t(TextTranslationKeys.newsletterView.editor.toolboxHelp)}
            </Alert>

            <div className={cx(style.toolbox)}>
                {
                    elements(t, TextTranslationKeys).map((element) => (
                        <button
                            type="button"
                            key={element.key}
                            className={cx(style.button, style.toolboxItem)}
                            ref={(ref) => create(ref, element.component)}
                        >
                            <span className={cx(style.buttonText)}>
                                <img
                                    tabIndex="-1"
                                    className={cx(style.buttonIcon)}
                                    src={element.icon}
                                    alt=""
                                />
                                {element.name}
                            </span>
                        </button>
                    ))
                }
            </div>
        </>
    );
}
