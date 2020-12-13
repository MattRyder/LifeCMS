import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';

const styles = {
    value: css`
        margin-bottom: 1rem;
        font-weight: 500;
    `,
};

export default function CardValue({ children }) {
    return (
        <h3 className={cx(styles.value)}>{children}</h3>
    );
}

CardValue.propTypes = {
    children: PropTypes.oneOfType([
        PropTypes.arrayOf(PropTypes.node),
        PropTypes.node,
    ]).isRequired,
};
