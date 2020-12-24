import { attr, fk, Model } from 'redux-orm';
import { DELETE_AUDIENCE, FETCH_AUDIENCE, UPDATE_AUDIENCE_NAME } from 'redux/actions/AudienceActions';

export default class Audience extends Model {
    static reducer(action, modelType) {
        switch (action.type) {
        case FETCH_AUDIENCE: {
            modelType.upsert(action.payload);
            break;
        }
        case DELETE_AUDIENCE: {
            modelType.withId(action.payload).delete();
            break;
        }
        case UPDATE_AUDIENCE_NAME: {
            const { id, name } = action.payload;

            modelType.withId(id).name = name;

            break;
        }
        default:
            break;
        }
    }
}

Audience.modelName = 'Audience';

Audience.fields = {
    id: attr(),
    userId: fk('User', 'audiences'),
};
