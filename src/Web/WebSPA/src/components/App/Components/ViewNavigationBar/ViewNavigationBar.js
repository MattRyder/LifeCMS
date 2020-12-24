import React from 'react';
import { cx, css } from 'emotion';
import { useHistory } from 'react-router-dom';
import { Button } from 'reactstrap';
import { useTranslations } from 'hooks';

const style = css`
    padding: 1rem 0;
`;

export default function ViewNavigationBar() {
    const history = useHistory();

    const { t, TextTranslationKeys } = useTranslations();

    return (
        <div className={cx(style)}>
            <Button
                onClick={() => history.goBack()}
                color="Link"
            >
                &lsaquo;
                &nbsp;
                {t(TextTranslationKeys.common.back)}
            </Button>
        </div>
    );
}
