import React from 'react';
import { cx, css } from 'emotion';
import {
    Card, CardInsight, CardTitle, CardValue,
} from 'components/Util/DashboardCard';
import {
    useContentApi, useResize, useTranslations, useUser,
} from 'hooks';
import { sub, DateRange } from 'components/Util/Date';
import Chart, { ChartType } from 'components/Util/Chart';
import { fetchCampaigns } from 'redux/actions/CampaignActions';
import { fetchNewsletters } from 'redux/actions/NewsletterTemplateActions';
import { fetchUserProfiles } from 'redux/actions/UserProfileActions';
import { Sentiment } from 'components/Util/DashboardCard/CardInsight';
import { useSelector } from 'react-redux';
import { findUserCampaigns, findUserNewsletterTemplates, findUserUserProfiles } from 'redux/redux-orm/ORM';
import Header from './Header';
import useStatistic from './useStatistic';
import useChartData from './useChart';
import ActiveUserProfileCard from './ActiveUserProfileCard';

const styles = {
    homeView: css`
        display: flex;
        flex-direction: column;
        flex: 1;
        padding: 2rem;
    `,
    header: css`
        margin: 0.5rem;
    `,
    title: css`
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        padding: 1rem 0;
    `,
    titleText: css`
        font-size: 1.25rem;
        font-weight: bold;
        margin-bottom: 0;
    `,
    message: css`
        margin-bottom: 0;
    `,
    actionButton: css`
        margin-left: 0.35rem;
        margin-right: 0.35rem;
    `,
    copyActions: css`
        display: flex;
        flex-direction: row;
        justify-content: space-between;
    `,
    horizontalRule: css`
        width: 100%;
    `,
    charts: css`
        display: flex;
        margin: 0.5rem;
        > div { width: 100%; }
    `,
    cardGrid: css`
        display: flex;
        flex-wrap: wrap;
        width: 100%;

        @media(min-width: 993px) {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            grid-template-rows: repeat(2, 1fr);

            > :nth-child(1) { grid-area: 1 / 1 / 2 / 2; }
            > :nth-child(2) { grid-area: 1 / 2 / 2 / 3; }
            > :nth-child(3) { grid-area: 2 / 1 / 3 / 2; }
            > :nth-child(4) { grid-area: 2 / 2 / 3 / 3; } 
        }
    `,
    card: css`
        flex: 1;
        margin: 0.5rem;
    `,
};

const getSentiment = (value) => {
    if (value === 0) {
        return Sentiment.default;
    }

    return value > 0 ? Sentiment.positive : Sentiment.negative;
};

const getValuePercent = (value) => {
    let prefix = '';

    if (value !== 0) {
        prefix = value < 0 ? '-' : '+';
    }

    return `${prefix}${value}%`;
};

const CardGrid = ({ stats, comment }) => stats.map((stat) => {
    const {
        statistic: {
            resourcesCreatedInPeriod,
            changePercent,
        }, title,
    } = stat;

    return (
        <Card
            key={title}
            className={cx(styles.card)}
        >
            <CardTitle>{title}</CardTitle>
            <CardValue>{resourcesCreatedInPeriod}</CardValue>
            <CardInsight
                comment={comment}
                sentiment={getSentiment(changePercent)}
                value={getValuePercent(changePercent)}
            />
        </Card>
    );
});

const Charts = ({ charts }) => charts.map(({ title, key, value }) => (
    <div className={styles.charts} key={title}>
        <Card>
            <CardTitle>{title}</CardTitle>
            {value && (
                <Chart
                    key={key}
                    options={value.options}
                    series={value.series}
                    type={ChartType.bar}
                    width="100%"
                    height={320}
                />
            )}
        </Card>
    </div>
));

const useDashboardStatistic = ({
    title, key, dateRange, collection, chart: { startDate, endDate },
}) => ({
    title,
    statistic: useStatistic({ collection, dateRange }),
    chart: useChartData({
        dateRange,
        endDate,
        key,
        collection,
        startDate,
        title,
    }),
});

export default function HomeView() {
    const { width: windowWidth } = useResize();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const userCampaigns = useSelector((state) => findUserCampaigns(state, userId));

    const userProfiles = useSelector((state) => findUserUserProfiles(state, userId));

    const newsletterTemplates = useSelector((state) => findUserNewsletterTemplates(state, userId));

    useContentApi(
        () => fetchCampaigns(accessToken, userId),
        accessToken,
    );

    useContentApi(
        () => fetchNewsletters(accessToken, userId),
        accessToken,
    );

    useContentApi(
        () => fetchUserProfiles(accessToken, userId),
        accessToken,
    );

    const statisticDefaults = {
        chart: {
            startDate: sub(new Date(), DateRange.week, 4),
            endDate: new Date(),
        },
        key: windowWidth,
        dateRange: DateRange.week,
    };

    const statistics = [
        useDashboardStatistic({
            title: t(TextTranslationKeys.homeView.statistics.campaignCreated),
            collection: userCampaigns,
            ...statisticDefaults,
        }),
        useDashboardStatistic({
            title: t(TextTranslationKeys.homeView.statistics.userProfilesCreated),
            collection: userProfiles,
            ...statisticDefaults,
        }),
        useDashboardStatistic({
            title: t(TextTranslationKeys.homeView.statistics.templatesCreated),
            collection: newsletterTemplates,
            ...statisticDefaults,
        }),
    ];

    return (
        <div className={cx(styles.homeView)}>
            <div className={styles.title}>
                <div className={styles.header}>
                    <Header />
                </div>

                <hr className={styles.horizontalRule} />

                <div className={cx(styles.cardGrid)}>
                    <CardGrid
                        comment={t(TextTranslationKeys.homeView.sinceLastWeek)}
                        stats={statistics}
                    />
                    <ActiveUserProfileCard
                        className={cx(styles.card)}
                        name={userProfiles.length > 0 ? userProfiles[0].name : ''}
                    />
                </div>

                <Charts charts={statistics.map((stat) => stat.chart)} />
            </div>
        </div>
    );
}
