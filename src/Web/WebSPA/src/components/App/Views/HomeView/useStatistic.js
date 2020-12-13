import { DateRange, sub } from 'components/Util/Date';
import { dashboardStatistic } from './DashboardStatistics';

export default function useStatistic({ collection, dateRange }) {
    const dateOneWeekAgo = sub(new Date(), DateRange.week, 1);

    return dashboardStatistic(collection, dateOneWeekAgo, dateRange);
}
