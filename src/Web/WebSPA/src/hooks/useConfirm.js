import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';

export const SweetAlert = withReactContent(Swal);

export default () => ({ SweetAlert });
