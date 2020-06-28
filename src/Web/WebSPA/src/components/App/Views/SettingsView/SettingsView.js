import React from 'react';
import { useRouteMatch } from 'react-router';
import { useTranslations } from '../../../../hooks';
import { Icons } from '../../Iconography/Icon';
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
    [
        {
            icon: Icons.attentionTriangle,
            text: t(TextTranslationKeys.settingsView.menu.newsletters),
            href: `${path}/newsletters`,
        },
    ],
];

export default function () {
    const { t, TextTranslationKeys } = useTranslations();

    const match = useRouteMatch();

    const menuItemGroups = getMenuItemGroups(match.path, t, TextTranslationKeys);

    return (
        <div className="settings-view">
            <SettingsMenuComponent groups={menuItemGroups} />
            <SettingsPaneComponent match={match} />
            {/* Help container as a drawer in the final third, with the ability to be hidden */}
        </div>
    );
}
