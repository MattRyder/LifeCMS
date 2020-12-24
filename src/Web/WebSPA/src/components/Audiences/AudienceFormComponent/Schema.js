import * as Yup from 'yup';

export const InitialValues = {
    name: '',
};

export default Yup.object().shape({
    name: Yup.string().required(),
});
