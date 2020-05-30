import * as Yup from 'yup';

export const InitialValues = {
    text: '',
};

export default Yup.object().shape({
    text: Yup
        .string()
        .trim()
        .required(),
});
