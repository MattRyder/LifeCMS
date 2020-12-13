import ResourceCreatedAtChartData from './ChartStatistics';

export default function useChartData({
    collection, startDate, endDate, dateRange, key, title,
}) {
    return {
        key,
        title,
        value: ResourceCreatedAtChartData(
            '',
            collection,
            startDate,
            endDate,
            dateRange,
        ),
    };
}
