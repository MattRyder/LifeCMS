import { add, formatDate, DateFormats } from 'components/Util/Date';
import { resourcesCreated } from './DashboardStatistics';

function makeChartConfig({ yMaxScaleFactor, keyValues, dataDescription }) {
    const categories = Object.keys(keyValues);

    const id = `chart-${new Date().getTime()}`;

    const series = [{
        name: dataDescription,
        data: Object.values(keyValues),
    }];

    const largestValue = Object
        .values(keyValues)
        .reduce((a, b) => Math.max(a, b));

    return {
        options: {
            chart: {
                id,
            },
            xaxis: {
                categories,
            },
            yaxis: {
                max: yMaxScaleFactor * largestValue,
            },
            tooltip: {
                enabled: false,
            },
        },
        series,
    };
}

function createDataForDate({ resourceArray, startDate, endDate }) {
    const key = formatDate(startDate, DateFormats.shortDayMonth);

    const data = resourcesCreated(
        resourceArray,
        startDate,
        endDate,
    );

    return {
        key,
        data,
    };
}

function makeKeyValues({
    resourceArray, startDate, endDate, dateRange,
}) {
    let currentDate = startDate;

    const keyValues = {};

    while (currentDate < endDate) {
        const nextDate = add(currentDate, dateRange, 1);

        const { key, data } = createDataForDate({
            resourceArray,
            startDate: currentDate,
            endDate: nextDate,
        });

        keyValues[key] = data;

        currentDate = nextDate;
    }

    return keyValues;
}

export default function ResourceCreatedAtChartData(
    description, resourceArray, startDate, endDate, dateRange,
) {
    const keyValues = makeKeyValues({
        resourceArray,
        startDate,
        endDate,
        dateRange,
    });

    return makeChartConfig({
        keyValues,
        dataDescription: description,
        yMaxScaleFactor: 1.25,
    });
}
