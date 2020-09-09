import * as Yup from 'yup';
import addHours from 'date-fns/addHours';

export const InitialValues = {
    newsletterTemplateId: '',
    userProfileId: '',
    subjectLineText: '',
    previewText: '',
    scheduledDate: addHours(new Date(), 2),
    useSubscriberTimeZone: false,
};

export default Yup.object().shape({
    newsletterTemplateId: Yup.string().required(),
    userProfileId: Yup.string().required(),
    subjectLineText: Yup.string().required(),
    previewText: Yup.string().required(),
    scheduledDate: Yup.date().required(),
    useSubscriberTimeZone: Yup.boolean(),
});
