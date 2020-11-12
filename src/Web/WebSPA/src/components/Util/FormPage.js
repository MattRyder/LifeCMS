import PropTypes from 'prop-types';
import ViewNavigationBar from 'components/App/Components/ViewNavigationBar/ViewNavigationBar';
import { css, cx } from 'emotion';
import React from 'react';
import { boxShadow } from 'theme';

const styles = {
    main: css`
        display: flex;
        flex-direction: column;
        width: 100%;
    `,
    page: css`
        padding: 2rem;
    `,
    header: css`
        font-size: 1.5rem;
        line-height: 3rem;
    `,
    form: css`
        ${boxShadow('rgba(0, 0, 0, 0.05)')}
        align-items: center;
        display: flex;
        flex-direction: column;
        width: 100%;
        padding: 1rem;
    `,
};

export default function FormPage({
    title, children,
}) {
    return (
        <div className={cx(styles.main)}>
            <ViewNavigationBar />

            <div className={cx(styles.page)}>
                <div className={cx(styles.header)}>
                    <span>{title}</span>
                </div>

                {children}
            </div>
        </div>
    );
}

FormPage.propTypes = {
    title: PropTypes.string.isRequired,
    children: PropTypes.oneOfType([
        PropTypes.arrayOf(PropTypes.node),
        PropTypes.node,
    ]).isRequired,
};
