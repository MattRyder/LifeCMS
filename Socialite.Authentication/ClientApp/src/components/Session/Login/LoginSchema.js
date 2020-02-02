import * as Yup from 'yup';

export default Yup.object().shape({
    emailAddress: Yup
        .string()
        .email('Please enter a valid email address')
        .required('Please enter your email address'),
    password: Yup
        .string()
        .required('Please enter your password'),
});

