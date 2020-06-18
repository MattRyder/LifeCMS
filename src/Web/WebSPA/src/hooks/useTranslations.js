import { useTranslation } from 'react-i18next';
import TextTranslationKeys from '../i18n/TextTranslationKeys';

export default function () {
    const { t } = useTranslation();

    return { t, TextTranslationKeys };
}
