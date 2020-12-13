import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';

const styles = {
    insight: css`
        color: rgba(0, 0, 0, 0.54);
    `,
    default: css`
        background-color: hsla(0, 5%, 58%, 0.1);
        color: hsl(0, 2%, 58%);
        border-radius: 3px;
        padding: 4px;
        margin-right: 12px;
    `,
    positive: css`
        background-color: hsla(120, 90%, 45%, 0.1);
        color: hsl(120, 89.6%, 45%);

    `,
    negative: css`
        background-color: hsla(4, 90%, 35%, 0.1);
        color: hsl(4, 89.6%, 30%);
    `,
};

export const Sentiment = {
    positive: Symbol('positive'),
    negative: Symbol('negative'),
    default: Symbol('default'),
};

export default function CardInsight({ value, sentiment, comment }) {
    return (
        <h6 className={cx(styles.insight)}>
            {value && (
                <span className={cx({
                    [styles.default]: true,
                    [styles.positive]: sentiment === Sentiment.positive,
                    [styles.negative]: sentiment === Sentiment.negative,
                })}
                >
                    {value}
                </span>
            )}
            {comment}
        </h6>
    );
}

CardInsight.propTypes = {
    value: PropTypes.oneOfType([
        PropTypes.string,
        PropTypes.number,
    ]),
    sentiment: PropTypes.symbol,
    comment: PropTypes.string,
};

CardInsight.defaultProps = {
    value: '',
    sentiment: Sentiment.default,
    comment: '',
};
