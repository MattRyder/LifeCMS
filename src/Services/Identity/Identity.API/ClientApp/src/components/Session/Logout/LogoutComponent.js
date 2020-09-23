import React from 'react';
import { useLocation } from 'react-router';
import { Button } from 'reactstrap';
import useTranslations from 'hooks/useTranslations';
import { getParamFromSearch } from '../../../QueryString';

import '../Session.scss';

export default function LogoutComponent() {
    const { search } = useLocation();

    const logoutId = getParamFromSearch(search, 'logoutId');

    const { t, TextTranslationKeys } = useTranslations();

    return (
        <div className="session-form">
            <div className="logout">
                <h2>{t(TextTranslationKeys.logout.notice)}</h2>

                <p>{t(TextTranslationKeys.logout.confirmMessage)}</p>

                <form action="/api/v1/accounts/logout" method="post">
                    <input type="hidden" name="logoutId" value={logoutId} />

                    <Button color="primary">
                        {t(TextTranslationKeys.logout.confirmButton)}
                    </Button>
                </form>
            </div>
        </div>
    );
}
