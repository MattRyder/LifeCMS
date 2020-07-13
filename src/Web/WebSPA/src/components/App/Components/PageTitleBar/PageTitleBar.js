import React from 'react';

import './PageTitleBar.scss';

export default function PageTitleBar({ children }) {
    return (
        <div className="page-title-bar">
            {children}
        </div>
    );
}
