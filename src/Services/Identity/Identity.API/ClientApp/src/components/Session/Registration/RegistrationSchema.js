import * as Yup from 'yup';

export const InitialValues = {
    emailAddress: '',
    emailAddressConfirmation: '',
    password: '',
};

export default Yup.object().shape({
    emailAddress: Yup
        .string()
        .email('Please enter a valid email address')
        .required('Please enter an email address'),
    emailAddressConfirmation: Yup
        .string()
        .email('Please enter a valid email address')
        .oneOf([Yup.ref('emailAddress'), null], 'Email addresses must match')
        .required('Please confirm your email address'),
    password: Yup
        .string()
        .required('Please enter a password'),
});
