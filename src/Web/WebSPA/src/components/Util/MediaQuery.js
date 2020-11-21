import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import { useMediaQuery } from 'react-responsive';

export default function MediaQuery({
    mediaQuery, activeRender: ActiveRender, inactiveRender: InactiveRender, handleChange,
}) {
    const isActive = useMediaQuery({
        query: mediaQuery,
    });

    useEffect(() => {
        handleChange(isActive);
    }, [handleChange, isActive]);

    return isActive ? <ActiveRender /> : <InactiveRender />;
}

MediaQuery.propTypes = {
    mediaQuery: PropTypes.string.isRequired,
    activeRender: PropTypes.node,
    inactiveRender: PropTypes.node,
    handleChange: PropTypes.func.isRequired,
};

MediaQuery.defaultProps = {
    activeRender: () => {},
    inactiveRender: () => {},
};
