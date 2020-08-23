/** @jsx jsx */
import { css, jsx } from '@emotion/core';

export default function ComponentWrapper({
    fontSize,
    padding,
    isSelected,
    children,
}) {
    const style = {
        fontSize: `${fontSize}rem`,
        padding: `${padding[0]}rem ${padding[1]}rem ${padding[2]}rem ${padding[3]}rem`,
    };
    return (
        <div
            className={`page-component ${isSelected ? 'is-selected' : ''}`}
            css={css(style)}
        >
            {children}
        </div>
    );
}
