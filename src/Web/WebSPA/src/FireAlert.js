import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';

const SweetAlert = withReactContent(Swal);

const FireAlert = (
    options,
    onConfirm = () => {},
    onCancel = () => {},
) => SweetAlert
    .fire(options)
    .then((result) => ((result.value) ? onConfirm : onCancel)());

export const FireConfirmAlert = (onConfirm, onCancel) => FireAlert({
    title: 'Are you sure?',
    text: "You won't be able to reverse this action.",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
}, onConfirm, onCancel);

export default FireAlert;
