import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { css } from 'emotion';
import { Checkmark } from 'react-checkmark';
import { confirmSubscriber } from 'redux/actions/SubscriberActions';
import theme from 'theme';
import { useQuery, useTranslations } from 'hooks';
import LanguageSelect from 'components/Util/LanguageSelect/LanguageSelect';
import Icon, { Icons } from 'components/App/Iconography/Icon';
import { ReactComponent as Loader } from './Loader.svg';

const styles = {
    container: css`
        display: flex;
        flex-direction: column !important;
        align-items: center;
        justify-content: center;
        background-color: ${theme.colors.modal.background};
    `,
    message: css`
        align-items: center;
        background-color: ${theme.colors.modal.message};
        border-radius: 10px;
        display: flex;
        justify-content: center;
        flex-direction: column;
        text-align: center;
        padding: 1rem;

        @media (min-width: 480px) {
            padding: 3rem;
        }
    `,
    checkmark: css`
        padding: 1rem;
    `,
    warningIcon: css`
        color: #ffc107;
    `,
    loader: css`
        height: auto;
        min-width: 5rem;
        padding: 1rem;
    `,
    languageSelect: css`
        display: flex;
        flex: 1;
        justify-content: end;
    `,
};

const IconMessage = ({ children, title, text }) => (
    <>
        <div className={styles.checkmark}>
            {children}
        </div>
        <h1>{title}</h1>
        <p>{text}</p>
    </>
);

IconMessage.propTypes = {
    children: PropTypes.oneOfType([PropTypes.node]).isRequired,
    text: PropTypes.string,
    title: PropTypes.string.isRequired,
};

IconMessage.defaultProps = {
    text: '',
};

const ActionState = {
    loading: Symbol('loading'),
    warning: Symbol('warning'),
    success: Symbol('success'),
};

export default function SubscriberConfirmView() {
    const { t, TextTranslationKeys } = useTranslations();

    const audienceId = useQuery('audienceId');

    const subscriberToken = useQuery('subscriberToken');

    const [state, setState] = useState({
        state: ActionState.loading,
        error: undefined,
    });

    useEffect(() => {
        async function fetchData() {
            try {
                await confirmSubscriber(
                    audienceId,
                    subscriberToken,
                );

                setState({
                    state: ActionState.success,
                    error: undefined,
                });
            } catch (error) {
                setState({
                    state: ActionState.warning,
                    error: error.response.data.errors[0],
                });
            }
        }

        fetchData();
    }, [audienceId, subscriberToken]);

    const PageContent = () => {
        switch (state.state) {
        case ActionState.warning:
            return (
                <IconMessage
                    title={t(TextTranslationKeys.subscriberConfirmView.warningText)}
                    text={state.error}
                >
                    <Icon
                        size="2x"
                        className={styles.warningIcon}
                        icon={Icons.attentionTriangle}
                    />
                </IconMessage>
            );
        case ActionState.success:
            return (
                <IconMessage
                    title={t(TextTranslationKeys.subscriberConfirmView.title)}
                    text={t(TextTranslationKeys.subscriberConfirmView.text)}
                >
                    <Checkmark />
                </IconMessage>
            );
        case ActionState.loading:
        default:
            return (
                <IconMessage
                    title={t(TextTranslationKeys.subscriberConfirmView.loadingText)}
                >
                    <Loader className={styles.loader} />
                </IconMessage>
            );
        }
    };

    return (
        <div className={styles.container}>
            <div>
                <div className={styles.message}>
                    <PageContent />
                </div>
                <div className={styles.languageSelect}>
                    <LanguageSelect showGlobeIcon={false} />
                </div>
            </div>
        </div>
    );
}
