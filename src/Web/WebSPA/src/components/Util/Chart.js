import React from 'react';
import PropTypes from 'prop-types';
import ApexChart from 'react-apexcharts';

export const ChartType = {
    bar: 'bar',
    line: 'line',
};

export default function Chart({
    className, options, series, height, width, type,
}) {
    return (
        <ApexChart
            className={className}
            height={height}
            options={options}
            series={series}
            type={type}
            width={width}
        />
    );
}

Chart.propTypes = {
    className: PropTypes.string,
    height: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string,
    ]).isRequired,
    options: PropTypes.shape({
        chart: PropTypes.shape({
            id: PropTypes.string.isRequired,
        }),
        xaxis: PropTypes.shape({
            categories: PropTypes.arrayOf(PropTypes.oneOfType([
                PropTypes.number,
                PropTypes.string,
            ])),
        }),
    }).isRequired,
    series: PropTypes.arrayOf(PropTypes.shape({
        name: PropTypes.string,
        data: PropTypes.arrayOf(PropTypes.number),
    })).isRequired,
    type: PropTypes.oneOf(Object.keys(ChartType)).isRequired,
    width: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string,
    ]).isRequired,
};

Chart.defaultProps = {
    className: '',
};
