import {
    addDays,
    eachDayOfInterval,
    format, getDate, parseISO, startOfWeek,
    isWithinInterval,
    addMonths,
    addWeeks,
    addYears,
    subDays,
    subMonths,
    subWeeks,
    subYears,
} from 'date-fns';
import {
    de, enGB, es, fr, it,
} from 'date-fns/locale';
import { getCurrentLanguage } from '../../i18n';

export const DateFormats = {
    timestamp: 'E, MMMM dd hh:mm a',
    shortDayOfWeek: 'iii',
    month: 'MMMM',
    year: 'yyyy',
    monthYear: 'MMMM yyyy',
    shortDayMonth: 'dd/MM',
};

export const DateRange = {
    day: Symbol('day'),
    month: Symbol('month'),
    week: Symbol('week'),
    year: Symbol('year'),
};

const getLocale = () => {
    const localesTable = {
        en: enGB,
        de,
        es,
        fr,
        it,
    };

    return localesTable[getCurrentLanguage()];
};

function processInput(dateInput) {
    if (typeof dateInput === 'string') {
        return parseISO(dateInput);
    }

    if (dateInput instanceof Date) {
        return dateInput;
    }

    throw new Error('Date input not in expected format: String or Date.');
}

function parseDate(date, dateFormat) {
    return format(
        processInput(date),
        dateFormat,
        {
            locale: getLocale(),
        },
    );
}

export function formatTimestampDate(dateString) {
    return dateString ? parseDate(dateString, DateFormats.timestamp) : '';
}

export function formatDate(date, dateFormat) {
    return date ? parseDate(date, dateFormat) : '';
}

export function getDayOfWeekShortName(date) {
    return date ? parseDate(date, DateFormats.shortDayOfWeek) : '';
}

export function getDayOfMonth(date) {
    return date ? getDate(date) : '';
}

export function getStartOfWeek(date) {
    return date ? startOfWeek(date) : undefined;
}

export function getDateRange(startDate, endDate) {
    return startDate && endDate
        ? eachDayOfInterval({
            start: startDate,
            end: endDate,
        })
        : undefined;
}

export function addDaysToDate(date, days) {
    return date ? addDays(date, days) : undefined;
}

export function getDateRangeString(startDate, endDate) {
    if (startDate && endDate) {
        const isSameYear = startDate.getYear() === endDate.getYear();

        const isSameMonth = startDate.getMonth() === endDate.getMonth();

        const endMonth = `- ${parseDate(endDate, DateFormats.month)}`;

        if (isSameYear) {
            return `
            ${parseDate(startDate, DateFormats.month)} 
            ${!isSameMonth ? endMonth : ''} 
            ${parseDate(endDate, DateFormats.year)}`;
        }

        return `
        ${parseDate(startDate, DateFormats.monthYear)} 
        - 
        ${parseDate(endDate, DateFormats.monthYear)}`;
    }

    return undefined;
}

export function isBetweenDateRange(date, startDate, endDate) {
    if (date && startDate && endDate) {
        return isWithinInterval(
            date,
            {
                start: startDate,
                end: endDate,
            },
        );
    }

    return false;
}

export function add(date, dateRange, amount) {
    if (date && dateRange && amount) {
        switch (dateRange) {
        case DateRange.day:
            return addDays(date, amount);
        case DateRange.month:
            return addMonths(date, amount);
        case DateRange.week:
            return addWeeks(date, amount);
        case DateRange.year:
            return addYears(date, amount);
        default:
            return undefined;
        }
    }

    return undefined;
}

export function sub(date, dateRange, amount) {
    if (date && dateRange && amount) {
        switch (dateRange) {
        case DateRange.day:
            return subDays(date, amount);
        case DateRange.month:
            return subMonths(date, amount);
        case DateRange.week:
            return subWeeks(date, amount);
        case DateRange.year:
            return subYears(date, amount);
        default:
            return undefined;
        }
    }

    return undefined;
}
