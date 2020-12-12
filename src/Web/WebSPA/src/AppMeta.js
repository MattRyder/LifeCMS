import React from 'react';
import PropTypes from 'prop-types';
import { Helmet } from 'react-helmet';

export default function AppMeta({
    title,
    description,
}) {
    return (
        <Helmet>
            <title>{title}</title>
            <meta name="description" content={description} />
        </Helmet>
    );
}

AppMeta.propTypes = {
    title: PropTypes.string,
    description: PropTypes.string,
};

AppMeta.defaultProps = {
    title: '',
    description: '',
};
