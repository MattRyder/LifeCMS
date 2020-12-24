import { attr, fk, Model } from 'redux-orm';
import { FETCH_SUBSCRIBER } from 'redux/actions/SubscriberActions';

export default class Subscriber extends Model {
    static reducer(action, modelType) {
        switch (action.type) {
        case FETCH_SUBSCRIBER: {
            modelType.upsert(action.payload);
            break;
        }
        default:
            break;
        }
    }
}

Subscriber.modelName = 'Subscriber';

Subscriber.fields = {
    id: attr(),
    audienceId: fk('Audience', 'subscribers'),
};
