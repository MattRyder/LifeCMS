import React from 'react';
import { useRouteMatch } from 'react-router';
import { useTranslations } from '../../../../hooks';
import { Icons } from '../../Iconography/Icon';
import PageTitleBar from '../../Components/PageTitleBar/PageTitleBar';
import SettingsMenuComponent from './SettingsMenuComponent';
import SettingsPaneComponent from './SettingsPaneComponent';

import './SettingsView.scss';

const getMenuItemGroups = (path, t, TextTranslationKeys) => [
    [
        {
            icon: Icons.userEdit,
            text: t(TextTranslationKeys.settingsView.menu.userProfiles),
            href: `${path}/user-profiles`,
        },
    ],
];

export default function () {
    const { t, TextTranslationKeys } = useTranslations();

    const match = useRouteMatch();

    const menuItemGroups = getMenuItemGroups(match.path, t, TextTranslationKeys);

    return (
        <div className="settings-view">
            <PageTitleBar>
                <span>{t(TextTranslationKeys.common.settings)}</span>
            </PageTitleBar>
            <div className="menu-pane">
                <SettingsMenuComponent groups={menuItemGroups} />
                <SettingsPaneComponent match={match} />
            </div>
        </div>
    );
}
