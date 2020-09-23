import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import detector from 'i18next-browser-languagedetector';
import {
    de, en, es, fr, it,
} from './i18n/locales';

export const supportedLocales = ['en', 'de', 'es', 'fr', 'it'];

const resources = {
    en: {
        translation: en,
    },
    de: {
        translation: de,
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
    .use(detector)
    .use(initReactI18next)
    .init({
        resources,
        keySeparator: false,
        interpolation: {
            escapeValue: false,
        },
    });

export default i18n;
