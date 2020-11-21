import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import { boxShadow } from 'theme';
import { rgba } from 'polished';

const styles = {
    page: css`
        ${boxShadow(rgba(0, 0, 0, 0.05))}
        background-color: #fff;
        display: flex;
        flex-direction: column;
        width: 100%;
        padding: 1rem;
    `,
};

export default function Paper({ children }) {
    return (
        <div className={cx(styles.page)}>
            {children}
        </div>
    );
}

Paper.propTypes = {
    children: PropTypes.oneOfType([
        PropTypes.arrayOf(PropTypes.node),
        PropTypes.node,
    ]).isRequired,
};
