import * as Yup from 'yup';

export const InitialValues = {
    mood: '',
    text: '',
};

export default Yup.object().shape({
    mood: Yup.string().trim().max(1),
    text: Yup
        .string()
        .trim()
        .required(),
});
