import * as Yup from 'yup';

export const InitialValues = {
    subjectLineText: '',
    previewText: '',
};

export default Yup.object().shape({
    subjectLineText: Yup.string().required(),
    previewText: Yup.string().required(),
});
