import createOidcMiddleware, { createUserManager } from 'redux-oidc';
import UserManagerSettings from './Settings';

const userManager = createUserManager(UserManagerSettings, () => true, false, '/callback');

const shouldValidate = (_, action) => !action.type === 'DONT_VALIDATE';

export default createOidcMiddleware(userManager, shouldValidate);
