import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import { Link } from 'react-router-dom';

const styles = {
    main: css`
        display: flex;
        flex-direction: column;
        justify-content: center;
    `,
    content: css`
        align-items: center;
        display: flex;
        flex-direction: column;
        padding: 2rem;

        @media(min-width: 992px) {
            flex-direction: row;
        }
    `,
    message: css`
        padding: 3rem 1rem;
        flex: 1;

        @media(min-width: 992px) {
            padding: 1rem;
        }
    `,
    illustration: css`
        flex: 1;
        padding: 1rem;
        > svg {
            flex: 1;
            height: auto;
            width: 100%;
        }
    `,
};

export default function IntroView({
    ctaText,
    ctaTo,
    children,
    resourceDescription,
    resourceTitle,
}) {
    return (
        <div className={cx(styles.main)}>
            <div className={cx(styles.content)}>
                <div className={cx(styles.message)}>
                    <h1>{resourceTitle}</h1>
                    <p>{resourceDescription}</p>
                    <Link to={ctaTo}>
                        {ctaText}
                        &nbsp;
                        &rsaquo;
                    </Link>
                </div>

                <div className={cx(styles.illustration)}>
                    {children}
                </div>
            </div>
        </div>
    );
}

IntroView.propTypes = {
    ctaText: PropTypes.string.isRequired,
    ctaTo: PropTypes.string.isRequired,
    children: PropTypes.oneOfType([PropTypes.node]).isRequired,
    resourceDescription: PropTypes.string.isRequired,
    resourceTitle: PropTypes.string.isRequired,
};
