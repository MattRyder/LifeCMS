import { attr, fk, Model } from 'redux-orm';
import {
    DELETE_NEWSLETTER_TEMPLATE, FETCH_NEWSLETTER_TEMPLATE,
} from 'redux/actions/NewsletterTemplateActions';

export default class NewsletterTemplate extends Model {
    static reducer(action, modelType) {
        switch (action.type) {
        case FETCH_NEWSLETTER_TEMPLATE: {
            modelType.upsert(action.payload);
            break;
        }
        case DELETE_NEWSLETTER_TEMPLATE:
            modelType.withId(action.payload).delete();
            break;
        default:
            break;
        }
    }
}

NewsletterTemplate.modelName = 'NewsletterTemplate';

NewsletterTemplate.fields = {
    id: attr(),
    userId: fk('User', 'newsletterTemplates'),
};
