import React, { useState } from 'react';
import { cx, css } from 'emotion';
import Icon, { Icons } from 'components/App/Iconography/Icon';
import AttributesPanel from './AttributesPanel';
import Toolbox from './Toolbox';

const style = {
    menu: css`
        border-left: 1px solid rgba(0, 0, 0, 0.2);
        height: 100%;
        flex: 0.275;
    `,
    titlebar: css`
        background-color: #cdc9c3;
        display: flex;
        justify-content: center;
        padding: 0.75rem 0;
    `,
    topToolMenu: css`
        background-color: white;
        display: flex;
    `,
    topToolMenuItem: css`
        align-items: center;
        border: none;
        border-right: 1px solid rgba(0,0,0,0.124) !important; 
        border-bottom-width: 3px;
        border-bottom-style: solid;
        border-bottom-color: rgba(0, 0, 0, 0.15);
        display: flex;
        flex: 1;
        flex-direction: column;
        padding: 0.5rem;
    `,
    topToolMenuItemActive: css`
        background-color: #FCFCFC;
        border-bottom-color: blue;
    `,
};

const EditorMenuButton = ({ isActive, item, handleClick }) => (
    <button
        type="button"
        onClick={handleClick}
        className={cx(
            { [style.topToolMenuItem]: true },
            {
                [style.topToolMenuItemActive]: isActive,
            },
        )}
    >
        <Icon icon={item.icon} />
        <span className={cx(style.buttonText)}>{item.text}</span>
    </button>
);

export default function EditorMenu({ title = 'Editorial Board' }) {
    const editorMenuItems = [
        {
            key: 'create',
            icon: Icons.cog,
            text: 'Create',
            component: <Toolbox />,
        },
        {
            key: 'style',
            icon: Icons.clone,
            text: 'Style',
            component: <AttributesPanel />,
        },
    ];

    const [
        selectedMenuItem,
        setSelectedMenuItem,
    ] = useState(editorMenuItems[0]);

    return (
        <div className={cx(style.menu)}>
            <div className={cx(style.titlebar)}>
                {title}
            </div>

            <div className={style.topToolMenu}>
                {editorMenuItems.map((item) => (
                    <EditorMenuButton
                        key={item.key}
                        item={item}
                        isActive={selectedMenuItem && selectedMenuItem.key === item.key}
                        handleClick={() => setSelectedMenuItem(item)}
                    />
                ))}
            </div>

            {selectedMenuItem.component}
        </div>
    );
}
