import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';

const styles = {
    title: css`
        margin-bottom: 1rem;
        font-weight: 500;
    `,
};

export default function CardTitle({ children }) {
    return (
        <h6 className={cx(styles.title)}>{children}</h6>
    );
}

CardTitle.propTypes = {
    children: PropTypes.oneOfType([
        PropTypes.arrayOf(PropTypes.node),
        PropTypes.node,
    ]).isRequired,
};
