import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';

import 'assets/fonts/Inter/stylesheet.css';

const styles = {
    card: css`
    background-color: #fff;
    box-shadow: rgba(50, 50, 93, 0.025) 0px 2px 5px -1px, rgba(0, 0, 0, 0.05) 0px 1px 3px -1px;
    color: rgba(0, 0, 0, 0.85);
    padding: 1rem;
    font-family: Inter;
    `,
};

export default function Card({ className, children }) {
    return (
        <div className={cx(styles.card, className)}>
            {children}
        </div>
    );
}

Card.propTypes = {
    children: PropTypes.oneOfType([
        PropTypes.arrayOf(PropTypes.node),
        PropTypes.node,
    ]),
    className: PropTypes.string,
};

Card.defaultProps = {
    children: [],
    className: '',
};
