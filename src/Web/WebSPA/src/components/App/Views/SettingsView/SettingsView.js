import React from 'react';
import SettingsMenuComponent from './SettingsMenuComponent';
import { useTranslations } from '../../../../hooks';
import { Icons } from '../../Iconography/Icon';

import './SettingsView.scss';
import SettingsPaneComponent from './SettingsPaneComponent';

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

export default function ({ match: { path } }) {
    const { t, TextTranslationKeys } = useTranslations();

    const menuItemGroups = getMenuItemGroups(path, t, TextTranslationKeys);

    return (
        <div className="settings-view">
            <SettingsMenuComponent groups={menuItemGroups} />
            <SettingsPaneComponent path={path} />
        </div>
    );
}
