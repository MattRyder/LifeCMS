import { useTranslation } from 'react-i18next';
import i18next from 'i18next';
import TextTranslationKeys from '../i18n/TextTranslationKeys';

export default function () {
    const { t: pureT } = useTranslation();

    const t = (key) => {
        if (process.env.NODE_ENV !== 'production' && (
            !key || !i18next.exists(key)
        )) {
            console.warn(`i18n key does not have translation: ${key}`);
        }

        return pureT(key);
    };

    return { t, TextTranslationKeys };
}
