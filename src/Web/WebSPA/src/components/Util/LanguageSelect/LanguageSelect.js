import React, { useEffect, useState } from 'react';
import Flag from 'react-flagkit';
import {
    Dropdown, DropdownItem, DropdownMenu, DropdownToggle,
} from 'reactstrap';
import i18n from 'i18n';
import { useToggle } from 'hooks';
import { css, cx } from 'emotion';

const styles = {
    main: css`
        display: flex;
        align-items: center;
        padding: 0.5rem;
    `,
    dropdownToggle: css`
        margin: 1rem 0;

        &:after {
            margin-left: 1rem;
        }
    `,
    globalizationIcon: css`
        padding: 0.5rem;

    `,
    flagIcon: css`
        margin-right: 0.5rem;
    `,
};

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

const menuItems = langSet.map(({ language, flagCountry, displayName }) => (
    <DropdownItem
        key={language}
        onClick={() => i18n.changeLanguage(language)}
    >
        <Flag
            className={cx(styles.flagIcon)}
            country={flagCountry}
            size={18}
        />
        <span>{displayName}</span>
    </DropdownItem>
));

export default function LanguageSelect() {
    const [isOpen, toggleOpen] = useToggle(false);

    const [currentLangName, setCurrentLangName] = useState('');

    useEffect(() => {
        const userLang = i18n && i18n.language;

        const lang = langSet.find((l) => l.language === userLang) || langSet[0];

        setCurrentLangName(lang.displayName);
    }, [i18n.language]);

    return (
        <div className={cx(styles.main)}>
            <span
                role="img"
                aria-label="globe-icon"
                className={cx(styles.globalizationIcon)}
            >
                &#x1F310;
            </span>
            <Dropdown isOpen={isOpen} size="sm" toggle={toggleOpen}>
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
