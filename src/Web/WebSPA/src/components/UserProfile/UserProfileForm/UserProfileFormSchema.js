import * as Yup from 'yup';

export const InitialValues = {
    name: '',
    email: '',
    occupation: '',
    location: '',
    bio: '',
    avatarImageUrn: '',
    headerImageUrn: '',
};

export default Yup.object().shape({
    name: Yup.string().trim().required(),
    email: Yup.string().trim().email().nullable(),
    occupation: Yup.string().trim().nullable(),
    location: Yup.string().trim().nullable(),
    bio: Yup.string().trim().max(250).nullable(),
    avatarImageUrn: Yup.string().trim().nullable(),
    headerImageUrn: Yup.string().trim().nullable(),
});
