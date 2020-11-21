import React from 'react';
import PropTypes from 'prop-types';
import { cx, css } from 'emotion';
import { Button } from 'reactstrap';
import { Link } from 'react-router-dom';

export default function DetailRow({
    label, value, linkTo, linkText,
}) {
    const styles = {
        detailRow: css`
            padding: 1.5rem 0.85rem;
        `,
        detailRowInner: css`
            align-items: center;
            display: flex;
            flex-direction: row;
            justify-content: space-between;
        `,
    };

    return (
        <div className={cx(styles.detailRow)}>
            <h4>{label}</h4>
            <div className={cx(styles.detailRowInner)}>
                {value}
                {linkTo && (
                    <Button color="link" tag={Link} to={linkTo}>
                        {linkText}
                    </Button>
                )}
            </div>
        </div>
    );
}

DetailRow.propTypes = {
    label: PropTypes.string.isRequired,
    value: PropTypes.oneOfType([PropTypes.node]).isRequired,
    linkTo: PropTypes.string,
    linkText: PropTypes.string,
};

DetailRow.defaultProps = {
    linkTo: '#',
    linkText: '',
};
