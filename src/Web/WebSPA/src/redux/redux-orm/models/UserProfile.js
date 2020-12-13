import { attr, fk, Model } from 'redux-orm';
import { DELETE_USER_PROFILE, FETCH_USER_PROFILE } from 'redux/actions/UserProfileActions';

export default class UserProfile extends Model {
    static reducer(action, modelType) {
        switch (action.type) {
        case FETCH_USER_PROFILE: {
            modelType.upsert(action.payload);
            break;
        }
        case DELETE_USER_PROFILE:
            modelType.withId(action.payload).delete();
            break;
        default:
            break;
        }
    }
}

UserProfile.modelName = 'UserProfile';

UserProfile.fields = {
    id: attr(),
    userId: fk('User', 'userProfiles'),
};
