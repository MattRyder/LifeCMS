import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import {
    addDaysToDate, getDateRange, getStartOfWeek,
} from 'components/Util/Date';
import MonthYearRow from './MonthYearRow';
import DateItem from './DateItem';

export default function DateSelector({ initialDate, dayCount }) {
    const [currentDate, setCurrentDate] = useState(initialDate);

    const [dateItems, setDateItems] = useState([]);

    const styles = {
        dateSelector: css`
            display: flex;
            flex-direction: column;
        `,
        dateItems: css`
            display: flex;
            flex-direction: row;
            justify-content: space-evenly;
            flex-wrap: wrap;
        `,
    };

    useEffect(() => {
        const startOfWeekDate = getStartOfWeek(currentDate);

        const dateRange = getDateRange(
            startOfWeekDate,
            addDaysToDate(startOfWeekDate, dayCount),
        );

        setDateItems(dateRange);
    }, [currentDate, dayCount]);

    return (
        <div className={cx(styles.dateSelector)}>
            <MonthYearRow
                startDate={dateItems[0]}
                endDate={dateItems.length > 0 && dateItems[dateItems.length - 1]}
            />
            <div className={cx(styles.dateItems)}>
                {dateItems.map((date) => (
                    <DateItem
                        key={date.getTime()}
                        handleClick={() => setCurrentDate(date)}
                        date={date}
                        isSelected={date.getDate() === currentDate.getDate()}
                    />
                ))}
            </div>
        </div>
    );
}

DateSelector.propTypes = {
    initialDate: PropTypes.instanceOf(Date),
    dayCount: PropTypes.number,
};

DateSelector.defaultProps = {
    initialDate: new Date(),
    dayCount: 6,
};
