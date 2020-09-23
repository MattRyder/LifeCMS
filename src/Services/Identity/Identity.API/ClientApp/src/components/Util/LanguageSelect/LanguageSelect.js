import React, { useEffect, useState } from 'react';
import Flag from 'react-flagkit';
import {
    Dropdown, DropdownItem, DropdownMenu, DropdownToggle,
} from 'reactstrap';
import i18n from '../../../i18n';

import './LanguageSelect.scss';

export default function LanguageSelect() {
    const changeLanguage = (lang) => i18n.changeLanguage(lang);

    const langSet = [
        {
            language: 'en',
            flagCountry: 'GB',
            displayName: 'English (United Kingdom)',
        },
        {
            language: 'de',
            flagCountry: 'DE',
            displayName: 'Deutsch',
        },
        {
            language: 'fr',
            flagCountry: 'FR',
            displayName: 'Français (France)',
        },
        {
            language: 'es',
            flagCountry: 'ES',
            displayName: 'Español (España)',
        },
        {
            language: 'it',
            flagCountry: 'IT',
            displayName: 'Italiano',
        },
    ];

    const [isOpen, setIsOpen] = useState(false);

    const toggle = () => setIsOpen(!isOpen);

    const [currentLangName, setCurrentLangName] = useState('');

    useEffect(() => {
        const userLang = i18n && i18n.language;

        console.log(userLang);

        let lang = langSet.find((l) => l.language === userLang);

        if (!lang) {
            lang = langSet[0];
        }

        setCurrentLangName(lang.displayName);
    }, [i18n.language]);

    const menuItems = langSet.map(({ language, flagCountry, displayName }) => (
        <DropdownItem
            key={language}
            onClick={() => changeLanguage(language)}
        >
            <Flag country={flagCountry} size={18} />
            <span>{displayName}</span>
        </DropdownItem>
    ));

    return (
        <div className="language-select">
            <Dropdown isOpen={isOpen} size="sm" toggle={toggle}>
                <DropdownToggle color="link" caret>
                    {currentLangName}
                </DropdownToggle>
                <DropdownMenu>
                    {menuItems}
                </DropdownMenu>
            </Dropdown>
        </div>
    );
}
