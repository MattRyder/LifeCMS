import React from 'react';
import { useParams } from 'react-router';
import { cx, css } from 'emotion';
import { rgba } from 'polished';
import { boxShadow } from 'theme';
import { fetchNewsletters } from 'redux/actions/NewsletterTemplateActions';
import {
    useContentApi, useUser, useStateSelector, useTranslations,
} from 'hooks';
import DetailPage from 'components/Util/DetailPage/DetailPage';
import Meta from 'components/Util/DetailPage/Meta';
import formatTimestampDate from 'components/Util/Date';

const styles = {
    wrapper: css`
        display: flex;
        flex-direction: column;
    `,
    iframe: css`
        background-color: #fff;
        border: none;
        flex: 1;
        ${boxShadow(rgba(0, 0, 0, 0.05))}
        width: 100%;

        & > div {
            height: 100%;
            width: 100%;
        }
    `,
};

export default function NewsletterTemplatePreview() {
    const { id } = useParams();

    const { accessToken, userId } = useUser();

    const { t, TextTranslationKeys } = useTranslations();

    const newsletter = useStateSelector(
        userId,
        'newsletter',
        'newsletters',
        id,
    );

    useContentApi(
        () => fetchNewsletters(accessToken, userId),
        accessToken,
        userId,
    );

    const title = `${
        t(TextTranslationKeys.newsletterView.previewPageTitle)
    }: ${newsletter && newsletter.name}`;

    return (
        <DetailPage title={title}>
            <iframe
                className={cx(styles.iframe)}
                title={title}
                srcDoc={newsletter && newsletter.html}
                sandbox="allow-scripts"
            />
            <Meta keyValues={[
                {
                    label: t(TextTranslationKeys.common.createdAt),
                    value: formatTimestampDate(newsletter && newsletter.createdAt),
                },
                {
                    label: t(TextTranslationKeys.common.updatedAt),
                    value: formatTimestampDate(newsletter && newsletter.updatedAt),
                },
            ]}
            />
        </DetailPage>
    );
}
