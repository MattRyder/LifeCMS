import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import { lighten } from 'polished';
import theme from 'theme';

export default function Meta({ keyValues }) {
    const styles = {
        meta: css`
            display: flex;
            flex-direction: column;
            padding: 1rem 0;
            text-align: right;
        `,
        metaText: css`
            color: ${lighten(0.33, theme.colors.mainLink)};
            font-size: 0.75rem;
            margin: 0.25rem;
        `,
    };
    return (
        <div className={cx(styles.meta)}>
            { keyValues && keyValues.map(({ label, value }) => (
                <p key={label} className={cx(styles.metaText)}>
                    {`${label}: ${value}`}
                </p>
            ))}
        </div>
    );
}

Meta.propTypes = {
    keyValues: PropTypes.arrayOf(
        PropTypes.shape({
            label: PropTypes.string,
            value: PropTypes.string,
        }),
    ),
};

Meta.defaultProps = {
    keyValues: [],
};
