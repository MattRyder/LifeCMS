import Swal from 'sweetalert2';
import i18next from 'i18next';
import withReactContent from 'sweetalert2-react-content';
import TextTranslationKeys from 'i18n/TextTranslationKeys';

const SweetAlert = withReactContent(Swal);

const FireAlert = (
    options,
    onConfirm = () => {},
    onCancel = () => {},
) => SweetAlert
    .fire(options)
    .then((result) => ((result.value) ? onConfirm : onCancel)());

export const FireConfirmAlert = (onConfirm, onCancel) => FireAlert({
    title: i18next.t(TextTranslationKeys.confirm.areYouSure),
    text: i18next.t(TextTranslationKeys.confirm.message),
    confirmButtonText: i18next.t(TextTranslationKeys.confirm.ok),
    cancelButtonText: i18next.t(TextTranslationKeys.confirm.cancel),
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
}, onConfirm, onCancel);

export default FireAlert;
