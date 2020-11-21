import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import detector from 'i18next-browser-languagedetector';
import {
    de, en, es, fr, it,
} from './i18n/locales';

export const supportedLocales = [de, en, es, fr, it];

export const getCurrentLanguage = () => i18n.language;

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

const fallbackLng = process.env.NODE_ENV === 'production' ? 'en' : undefined;

i18n
    .use(detector)
    .use(initReactI18next)
    .init({
        resources,
        fallbackLng,
        keySeparator: false,
        interpolation: {
            escapeValue: false,
        },
    });

export default i18n;
