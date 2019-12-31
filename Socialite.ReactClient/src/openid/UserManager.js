import { createUserManager } from 'redux-oidc';
import userManagerSettings from './Settings';

export default createUserManager(userManagerSettings);