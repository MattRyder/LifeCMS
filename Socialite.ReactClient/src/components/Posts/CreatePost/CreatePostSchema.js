import * as Yup from 'yup';

export const InitialValues = {
    title: '',
    text: '',
};

export default Yup.object().shape({
    title: Yup.string().trim().required(),
    text: Yup
        .string()
        .trim()
        .required(),
});
