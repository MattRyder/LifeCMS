import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import { getDayOfMonth, getDayOfWeekShortName } from 'components/Util/Date';
import theme from 'theme';
import { lighten } from 'polished';

export default function DateItem({ date, isSelected, handleClick }) {
    const dayOfMonth = getDayOfMonth(date);

    const dayName = getDayOfWeekShortName(date);

    const styles = {
        container: css`
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            background-color: #fff;
            border-color: ${lighten(0.2, theme.colors.mainUnderBackground)};
            border-width: 2px;
            border-style: solid;
            padding: 0.25rem 1rem;
            border-radius: 7px;
            
            &:focus{ outline: none; }
        `,
        selected: css`
            border-color: ${lighten(0.2, theme.colors.mainAccent)}
        `,
        dateOfMonth: css`
            color: ${theme.colors.mainAccent};
            font-weight: bold;
        `,
        dayOfWeek: css`
            font-size: small;
        `,
    };

    return (
        <button
            className={cx({
                [styles.container]: true,
                [styles.selected]: isSelected,
            })}
            type="button"
            onClick={handleClick}
        >
            <span className={cx(styles.dateOfMonth)}>{dayOfMonth}</span>
            <span className={cx(styles.dayOfWeek)}>{dayName}</span>
        </button>
    );
}

DateItem.propTypes = {
    date: PropTypes.instanceOf(Date).isRequired,
    handleClick: PropTypes.func.isRequired,
    isSelected: PropTypes.bool,
};

DateItem.defaultProps = {
    isSelected: false,
};
