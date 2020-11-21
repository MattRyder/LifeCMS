/** @jsx jsx */
import { jsx } from '@emotion/core';
import { css, cx } from 'emotion';

const styles = {
    pageComponent: css`
        min-height: 5rem;
        border-width: 2px;
        border-color: rgba(25, 132, 255, 0.15);
        border-style: solid;
        p {
            margin: 0;
            padding: 0;
        }
    `,
    isSelected: css`
        border-color: #117cfa !important;
    `,
};

export default function ComponentWrapper({
    backgroundColor,
    color,
    fontSize,
    padding,
    isSelected,
    children,
}) {
    const userDefinedStyle = css({
        backgroundColor,
        color,
        fontSize: `${fontSize}rem`,
        padding: `${padding[0]}rem ${padding[1]}rem ${padding[2]}rem ${padding[3]}rem`,
    });

    return (
        <div
            className={cx(
                { [styles.pageComponent]: true },
                { [styles.isSelected]: isSelected },
                { [userDefinedStyle]: true },
            )}
        >
            {children}
        </div>
    );
}
