import { css, cx } from 'emotion';
import PropTypes from 'prop-types';
import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'reactstrap';

const styles = {
    content: css`
        display: flex;
        flex-direction:column;
        height: 100%;
        width: 100%;
        padding: 2rem;
    `,
    title: css`
        display: flex;
        justify-content: space-between;
        padding: 0.5rem 0;
    `,
    titleText: css`
        font-size: 1.25rem;
        margin-bottom: 0;
    `,
};

export default function ListView({
    title,
    ctaText,
    ctaLinkTo,
    children,
}) {
    return (
        <div className={cx(styles.content)}>
            <div className={cx(styles.title)}>
                <h2 className={cx(styles.titleText)}>
                    {title}
                </h2>
                <Button
                    tag={Link}
                    size="sm"
                    color="primary"
                    to={ctaLinkTo}
                >
                    {ctaText}
                </Button>
            </div>
            {children}
        </div>
    );
}

ListView.propTypes = {
    title: PropTypes.string.isRequired,
    ctaText: PropTypes.string.isRequired,
    ctaLinkTo: PropTypes.string.isRequired,
    children: PropTypes.oneOfType([
        PropTypes.arrayOf(PropTypes.node),
        PropTypes.node,
    ]).isRequired,
};
