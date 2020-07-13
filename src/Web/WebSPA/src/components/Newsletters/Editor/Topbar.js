import React from 'react';
import { Button } from 'reactstrap';
import { useEditor } from '@craftjs/core';

import './Topbar.scss';

export default function Topbar({ onSave, title }) {
    const { query } = useEditor();

    return (
        <div className="top-bar">
            <span>{title}</span>
            <Button
                type="submit"
                color="primary"
                onClick={() => onSave(query)}
            >
                Save
            </Button>
        </div>
    );
}
