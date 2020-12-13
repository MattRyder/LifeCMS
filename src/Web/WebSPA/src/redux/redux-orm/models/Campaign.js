import { attr, fk, Model } from 'redux-orm';
import { DELETE_CAMPAIGN, FETCH_CAMPAIGN } from 'redux/actions/CampaignActions';

export default class Campaign extends Model {
    static reducer(action, modelType) {
        switch (action.type) {
        case FETCH_CAMPAIGN: {
            modelType.upsert(action.payload);
            break;
        }
        case DELETE_CAMPAIGN: {
            modelType.withId(action.payload).delete();
            break;
        }
        default:
            break;
        }
    }
}

Campaign.modelName = 'Campaign';

Campaign.fields = {
    id: attr(),
    userId: fk('User', 'campaigns'),
};
