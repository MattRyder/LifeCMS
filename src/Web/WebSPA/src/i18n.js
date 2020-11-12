import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import {
    de, en, es, fr, it,
} from './i18n/locales';

export const supportedLocales = [de, en, es, fr, it];

const resources = {
    de: {
        translation: de,
    },
    en: {
        translation: en,
    },
    es: {
        translation: es,
    },
    fr: {
        translation: fr,
    },
    it: {
        translation: it,
    },
};

i18n
    .use(initReactI18next)
    .init({
        resources,
        lng: 'en',
        keySeparator: false,
        interpolation: {
            escapeValue: false,
        },
    });

export default i18n;
