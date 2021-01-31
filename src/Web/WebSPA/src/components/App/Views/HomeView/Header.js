import React from 'react';
import { css } from 'emotion';
import Icon, { Icons } from 'components/App/Iconography/Icon';
import { Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import { useTranslations } from 'hooks';
import { getMonthDay } from 'components/Util/Date';

const styles = {
    header: css`
        display: flex;
        flex-direction: row;
        justify-content: space-between;
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
    horizontalRule: css`
        width: 100%;
    `,
    card: css`
        margin-top: 0.5rem;
        margin-bottom: 0.5rem;
    `,
    cardContainer: css`
        display: flex:
        flex-wrap: wrap;
    `,
};

export default function Header() {
    const { t, TextTranslationKeys } = useTranslations();

    return (
        <>
            <div className={styles.header}>
                <h1 className={styles.titleText}>
                    {t(TextTranslationKeys.homeView.pageTitle)}
                </h1>

                <div>
                    <Button
                        outline
                        size="sm"
                        color="secondary"
                        className={styles.actionButton}
                    >
                        <Icon icon={Icons.settings} />
                    </Button>

                    <Button
                        outline
                        size="sm"
                        color="secondary"
                        className={styles.actionButton}
                    >
                        <Icon icon={Icons.sync} />
                    </Button>

                    <Button
                        className={styles.actionButton}
                        tag={Link}
                        size="sm"
                        color="primary"
                        to="#"
                    >
                        Today:
                        &nbsp;
                        {`${getMonthDay(new Date())}`}
                        &nbsp;
                        <Icon icon={Icons.caretDown} />
                    </Button>
                </div>
            </div>
            <p className={styles.messsage}>
                {t(TextTranslationKeys.homeView.greeting)}
            </p>
        </>
    );
}
