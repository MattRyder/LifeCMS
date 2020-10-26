import React from 'react';
import PropTypes from 'prop-types';

export default class ErrorBoundary extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            error: null,
        };

        this.renderChildren = this.renderChildren.bind(this);

        this.renderError = this.renderError.bind(this);
    }

    static getDerivedStateFromError(error) {
        return {
            error,
        };
    }

    componentDidCatch(error, errorInfo) {
        console.error(error);

        console.error(errorInfo);
    }

    renderChildren() {
        const { children } = this.props;

        return children;
    }

    renderError() {
        const { error } = this.state;

        return (
            <>
                <p>{error.message}</p>
            </>
        );
    }

    render() {
        const { error } = this.state;

        return error ? this.renderError() : this.renderChildren();
    }
}

ErrorBoundary.propTypes = {
    children: PropTypes.oneOfType([
        PropTypes.arrayOf(PropTypes.node),
        PropTypes.node,
    ]).isRequired,
};
