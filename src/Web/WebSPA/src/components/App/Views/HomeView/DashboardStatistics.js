import {
    add, DateRange, isBetweenDateRange, sub,
} from 'components/Util/Date';
import moize from 'moize';

function calculateResourceCreated(resourceArray, startDate, endDate) {
    if (resourceArray) {
        return resourceArray
            .filter((resource) => isBetweenDateRange(
                new Date(resource.createdAt), startDate, endDate,
            ))
            .length;
    }

    return 0;
}

export const resourcesCreated = moize(calculateResourceCreated);

function getComparisionInterval(startDate, dateRange) {
    if (!startDate) {
        return undefined;
    }

    return {
        start: sub(startDate, dateRange),
        end: sub(startDate, DateRange.day),
    };
}

function calculateDashboardStatistic(resourceArray, startDate, dateRange) {
    const resourcesCreatedInPeriod = resourcesCreated(
        resourceArray,
        startDate,
        add(startDate, dateRange, 1),
    );

    const comparisonInterval = getComparisionInterval(startDate, dateRange);

    const resourcesCreatedLastPeriod = resourcesCreated(
        resourceArray,
        comparisonInterval.start,
        comparisonInterval.end,
    );

    const changePercent = (
        resourcesCreatedInPeriod / Math.max(resourcesCreatedLastPeriod, 1)
    ) * 100.0;

    return {
        resourcesCreatedInPeriod,
        resourcesCreatedLastPeriod,
        changePercent,
    };
}

export const dashboardStatistic = moize(calculateDashboardStatistic);
