import React from 'react';
import PropTypes from 'prop-types';
import { useHistory } from 'react-router-dom';
import { Button } from 'reactstrap';

function ViewNavigationBar({
    showBackLink,
}) {
    const history = useHistory();

    return (
        <div className="view-navigation-bar">
            { showBackLink && (
                <Button
                    onClick={() => history.goBack()}
                    color="Link"
                >
                    &lsaquo; Back
                </Button>
            )}
        </div>
    );
}

ViewNavigationBar.propTypes = {
    showBackLink: PropTypes.bool,
};

ViewNavigationBar.defaultProps = {
    showBackLink: true,
};

export default ViewNavigationBar;
