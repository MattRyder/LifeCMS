import React from 'react';
import PropTypes from 'prop-types';
import ViewNavigationBar from 'components/App/Components/ViewNavigationBar/ViewNavigationBar';
import { css, cx } from 'emotion';

const styles = {
    detailPage: css`
        display: flex;
        flex-direction: column;
        width: 100%;
        padding: 1rem;
        flex: 1;
    `,
    header: css`
        font-size: 1.5rem;
        line-height: 3rem;
        padding: 0 1rem;
    `,
    children: css`
        display: flex;
        flex-direction:column;
        flex: 1;
        padding: 1rem;
    `,
};

export default function DetailPage({ title, children }) {
    return (
        <div className={cx(styles.detailPage)}>
            <ViewNavigationBar showBackLink />

            <div className={cx(styles.header)}>
                <span>{title}</span>
            </div>

            <div className={cx(styles.children)}>
                {children}
            </div>
        </div>
    );
}

DetailPage.propTypes = {
    title: PropTypes.string.isRequired,
    children: PropTypes.oneOfType([
        PropTypes.arrayOf(PropTypes.node),
        PropTypes.node,
    ]).isRequired,
};
