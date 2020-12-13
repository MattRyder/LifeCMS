import decodeToken from 'openid/Token';
import { USER_FOUND } from 'redux-oidc';
import { Model } from 'redux-orm';

export default class User extends Model {
    static reducer(action, modelType) {
        switch (action.type) {
        case USER_FOUND: {
            const { sub } = decodeToken(action.payload.access_token);

            modelType.upsert({ id: sub });

            break;
        }
        default:
            break;
        }
    }
}

User.modelName = 'User';
