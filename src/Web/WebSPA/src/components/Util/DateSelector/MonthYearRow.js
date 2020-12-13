import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import { getDateRangeString } from 'components/Util/Date';

export default function MonthYearRow({ startDate, endDate }) {
    const styles = {
        container: css`
            display: flex;
            justify-content: center;
        `,
    };

    const [dateRangeString, setDateRangeString] = useState('');

    useEffect(() => {
        const dateRangeStr = getDateRangeString(startDate, endDate);

        setDateRangeString(dateRangeStr);
    }, [startDate, endDate, dateRangeString]);

    return (
        <div className={cx(styles.container)}>
            <p>{dateRangeString}</p>
        </div>
    );
}

MonthYearRow.propTypes = {
    startDate: PropTypes.instanceOf(Date),
    endDate: PropTypes.instanceOf(Date),
};

MonthYearRow.defaultProps = {
    startDate: new Date(),
    endDate: new Date(),
};
