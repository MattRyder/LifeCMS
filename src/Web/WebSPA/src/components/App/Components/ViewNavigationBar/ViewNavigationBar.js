import React from 'react';
import { cx, css } from 'emotion';
import { useHistory } from 'react-router-dom';
import { Button } from 'reactstrap';

const style = css`
    padding: 1rem 0;
`;

export default function ViewNavigationBar() {
    const history = useHistory();

    return (
        <div className={cx(style)}>
            <Button
                onClick={() => history.goBack()}
                color="Link"
            >
                &lsaquo; Back
            </Button>
        </div>
    );
}
