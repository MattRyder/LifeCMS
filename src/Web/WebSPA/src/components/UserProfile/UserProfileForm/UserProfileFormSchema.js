import * as Yup from 'yup';

export const InitialValues = {
    name: '',
    email: '',
    occupation: '',
    location: '',
    bio: '',
    avatarImageUri: '',
    headerImageUri: '',
};

export default Yup.object().shape({
    name: Yup.string().trim().required(),
    email: Yup.string().trim().email(),
    occupation: Yup.string().trim(),
    location: Yup.string().trim(),
    bio: Yup.string().trim().max(250),
    avatarImageUri: Yup.string().trim().url(),
    headerImageUri: Yup.string().trim().url(),
});
