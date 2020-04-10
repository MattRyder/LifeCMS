import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import { de, en, es } from './i18n/locales';

export const supportedLocales = ['en', 'de', 'es'];

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
