import { format, parseISO } from 'date-fns';
import {
    de, enGB, es, fr, it,
} from 'date-fns/locale';
import { getCurrentLanguage } from '../../i18n';

export const TimestampDateFormat = 'E, MMMM dd hh:mm a';

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

function parseDate(date) {
    return format(
        parseISO(date),
        TimestampDateFormat,
        {
            locale: getLocale(),
        },
    );
}

export default function formatTimestampDate(date) {
    return date ? parseDate(date) : '';
}
